using EbuBridgeLmsSystem.Domain.Entities.Common;
using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Behaviours
{
    public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Any())
                {
                    var errorMessages = failures.Select(f => f.ErrorMessage).ToList();
                    var resultType = typeof(TResponse);

                    if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
                    {
                        var dataType = resultType.GenericTypeArguments[0];
                        var method = typeof(Result<>)
                            .MakeGenericType(dataType)
                            .GetMethod("Failure", new[] { typeof(Error), typeof(List<string>), typeof(ErrorType) });

                        return (TResponse)method.Invoke(null, new object[] {
                         Error.ValidationFailed,
                         errorMessages,
                         ErrorType.ValidationError
                     });
                    }

                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
