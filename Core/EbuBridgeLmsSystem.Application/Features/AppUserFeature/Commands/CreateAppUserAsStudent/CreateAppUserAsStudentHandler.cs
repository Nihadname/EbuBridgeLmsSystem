using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent
{
    public class CreateAppUserAsStudentHandler : IRequestHandler<CreateAppUserAsStudentCommand,Result<UserGetDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        public CreateAppUserAsStudentHandler(IMapper mapper, UserManager<AppUser> userManager, IEmailService emailService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        async Task<Result<UserGetDto>> IRequestHandler<CreateAppUserAsStudentCommand, Result<UserGetDto>>.Handle(CreateAppUserAsStudentCommand request, CancellationToken cancellationToken)
        {
            var appUserResult =await _userManager.CreateUser(request.RegisterDto, _unitOfWork, _emailService);
            if(!appUserResult.IsSuccess)
              return  Result<UserGetDto>.Failure(appUserResult.ErrorKey, appUserResult.Message, appUserResult.Errors, (ErrorType)appUserResult.ErrorType);

            return Result<UserGetDto>.Success(null);
        }
    }
}
