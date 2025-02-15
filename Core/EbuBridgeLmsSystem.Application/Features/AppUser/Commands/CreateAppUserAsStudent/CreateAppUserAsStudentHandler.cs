using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Student;
using EbuBridgeLmsSystem.Application.Interfaces;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUser.Commands.CreateAppUserAsStudent
{
    public class CreateAppUserAsStudentHandler : IRequestHandler<CreateAppUserAsStudentCommand,Result<UserGetDto>>
    {
        private  readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public CreateAppUserAsStudentHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        async Task<Result<UserGetDto>> IRequestHandler<CreateAppUserAsStudentCommand, Result<UserGetDto>>.Handle(CreateAppUserAsStudentCommand request, CancellationToken cancellationToken)
        {
            var registerDto = _mapper.Map<StudentRegistrationDto>(request);
            return Result<UserGetDto>.Success(null);
        }
    }
}
