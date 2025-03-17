using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.SendVerificationCode
{
    public class SendVerificationCodeHandler : IRequestHandler<SendVerificationCodeCommand, Result<Unit>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private  readonly IBackgroundJobClient _backgroundJobClient;
        public SendVerificationCodeHandler(UserManager<AppUser> userManager, IMapper mapper, IEmailService emailService, IBackgroundJobClient backgroundJobClient)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Result<Unit>> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var mappedSendVerificationCode=_mapper.Map<SendVerificationCodeDto>(request);
            var result=await _userManager.SendVerificationCode(mappedSendVerificationCode,_emailService,_backgroundJobClient);
            if (!result.IsSuccess)
                return Result<Unit>.Failure(result.Error, result.Errors, (ErrorType)result.ErrorType);
            return Result<Unit>.Success(Unit.Value);
           
        }
    }
}
