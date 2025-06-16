using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LanguageFeature.Commands.LanguageCreate;

public sealed class LanguageCreateHandler: IRequestHandler<LanguageCreateCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public LanguageCreateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(LanguageCreateCommand request, CancellationToken cancellationToken)
    {
        bool isTheNameTaken=await _unitOfWork.LanguageRepository.
            isExists(s=>s.Name.ToLower()==request.Name.ToLower());
        if (isTheNameTaken)
        {
            return Result<Unit>.Failure(Error.DuplicateConflict, null,ErrorType.BusinessLogicError);
        }

        Language newLanguage = new Language()
        {
            Name=request.Name,
        };
        await _unitOfWork.LanguageRepository.Create(newLanguage);   
        await _unitOfWork.SaveChangesAsync(cancellationToken);  
        return Result<Unit>.Success(Unit.Value, SuccessReturnType.Created);
    }
}