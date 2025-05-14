using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem;
using EbuBridgeLmsSystem.Domain.Enums;
using EbuBridgeLmsSystem.Domain.Repositories;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsSaasStudent
{
    public sealed class CreateAppUserAsSaasStudentHandler : IRequestHandler<CreateAppUserAsSaasStudentCommand, Result<Unit>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateAppUserAsSaasStudentHandler> _logger; 
        private readonly IBackgroundJobClient _backgroundJobClient;
        public CreateAppUserAsSaasStudentHandler(ILogger<CreateAppUserAsSaasStudentHandler> logger, IEmailService emailService, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IBackgroundJobClient backgroundJobClient)
        {
            _logger = logger;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Result<Unit>> Handle(CreateAppUserAsSaasStudentCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var appUserResult = await _userManager.CreateUser(request.RegisterDto, _unitOfWork, _emailService, _backgroundJobClient);
                if (!appUserResult.IsSuccess)
                    return Result<Unit>.Failure(appUserResult.Error, appUserResult.Errors, (ErrorType)appUserResult.ErrorType);
               var result= await _userManager.AddToRoleAsync(appUserResult.Data, RolesEnum.SaasLmsUser.ToString());
                if (!result.Succeeded)
                {
                    return Result<Unit>.Failure(Error.ValidationFailed, result.Errors.Select(s=>s.Description).ToList(), ErrorType.ValidationError);
                }
                var newSassStudent = new SaasStudent()
                {
                    AppUserId=appUserResult.Data.Id,
                };
                await _unitOfWork.SaasStudentRepository.Create(newSassStudent);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return Result<Unit>.Success(Unit.Value,SuccessReturnType.Created);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during user registration");
                return Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);

            }
        }
    }
}
