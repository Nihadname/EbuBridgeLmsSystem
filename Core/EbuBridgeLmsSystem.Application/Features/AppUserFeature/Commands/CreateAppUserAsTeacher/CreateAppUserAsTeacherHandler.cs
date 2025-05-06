using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Teacher;
using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Enums;
using EbuBridgeLmsSystem.Domain.Repositories;
using FluentValidation;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsTeacher
{
    public class CreateAppUserAsTeacherHandler:IRequestHandler<CreateAppUserAsTeacherCommand, Result<UserGetDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<CreateAppUserAsTeacherHandler> _logger;
        private readonly IBackgroundJobClient _backgroundJobClient;
        public CreateAppUserAsTeacherHandler(UserManager<AppUser> userManager, ILogger<CreateAppUserAsTeacherHandler> logger, IUnitOfWork unitOfWork, IEmailService emailService, IMapper mapper, IBackgroundJobClient backgroundJobClient)
        {
            _userManager = userManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _mapper = mapper;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Result<UserGetDto>> Handle(CreateAppUserAsTeacherCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                
                var appUserResult = await _userManager.CreateUser(request.RegisterDto, _unitOfWork, _emailService, _backgroundJobClient);
                if (!appUserResult.IsSuccess)
                    return Result<UserGetDto>.Failure(appUserResult.Error, appUserResult.Errors, (ErrorType)appUserResult.ErrorType);
                await _userManager.AddToRoleAsync(appUserResult.Data, RolesEnum.Teacher.ToString());
                request.TeacherCreateDto.AppUserId= appUserResult.Data.Id;
                var mappedTeacher=_mapper.Map<Teacher>(request.TeacherCreateDto);
                await _unitOfWork.TeacherRepository.Create(mappedTeacher);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                var mappedUser = _mapper.Map<UserGetDto>(appUserResult.Data);
                return Result<UserGetDto>.Success(mappedUser, SuccessReturnType.Created);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during user registration");
                return Result<UserGetDto>.Failure(Error.InternalServerError, null, ErrorType.SystemError);
            }
        }
    }
}
