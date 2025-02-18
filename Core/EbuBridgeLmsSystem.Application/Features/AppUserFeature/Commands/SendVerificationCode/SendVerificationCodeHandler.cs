using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.SendVerificationCode
{
    public class SendVerificationCodeHandler : IRequestHandler<SendVerificationCodeCommand, Result<Unit>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public SendVerificationCodeHandler(UserManager<AppUser> userManager, IMapper mapper, IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Result<Unit>> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var mappedSendVerificationCode=_mapper.Map<SendVerificationCodeDto>(request);
            var result=await _userManager.SendVerificationCode(mappedSendVerificationCode,_emailService);
            if (!result.IsSuccess)
                return Result<Unit>.Failure(result.ErrorKey, result.Message, result.Errors, (ErrorType)result.ErrorType);
            return Result<Unit>.Success(Unit.Value);
           
        }
    }
}
