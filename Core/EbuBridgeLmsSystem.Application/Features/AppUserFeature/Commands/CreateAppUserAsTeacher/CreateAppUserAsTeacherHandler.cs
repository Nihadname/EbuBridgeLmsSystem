using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Teacher;
using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
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
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<TeacherCreateDto> _teacherValidator;
        private readonly IBackgroundJobClient _backgroundJobClient;
        public CreateAppUserAsTeacherHandler(UserManager<AppUser> userManager, ILogger<CreateAppUserAsTeacherHandler> logger, IValidator<RegisterDto> registerValidator, IValidator<TeacherCreateDto> teacherValidator, IUnitOfWork unitOfWork, IEmailService emailService, IMapper mapper, IBackgroundJobClient backgroundJobClient)
        {
            _userManager = userManager;
            _logger = logger;
            _registerValidator = registerValidator;
            _teacherValidator = teacherValidator;
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
                var registerValidationResult = await _registerValidator.ValidateAsync(request.RegisterDto, cancellationToken);
                var teacherValidationResult = await _teacherValidator.ValidateAsync(request.TeacherCreateDto, cancellationToken);
                List<FluentValidation.Results.ValidationResult> validations = new();
                validations.Add(registerValidationResult);
                validations.Add(teacherValidationResult);
                if (validations.Any(s => s.IsValid == false))
                {
                    var errors = new List<string>();
                    foreach (var validationResult in validations)
                    {
                        var errorsInValidation = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        foreach (var error in errorsInValidation)
                        {
                            errors.Add(error);
                        }

                    }
                    return Result<UserGetDto>.Failure(null, errors,ErrorType.ValidationError);
                }
                var appUserResult = await _userManager.CreateUser(request.RegisterDto, _unitOfWork, _emailService, _backgroundJobClient);
                if (!appUserResult.IsSuccess)
                    return Result<UserGetDto>.Failure(appUserResult.Error, appUserResult.Errors, (ErrorType)appUserResult.ErrorType);
                await _userManager.AddToRoleAsync(appUserResult.Data, RolesEnum.Teacher.ToString());
                request.TeacherCreateDto.AppUserId= appUserResult.Data.Id;
                var mappedTeacher=_mapper.Map<Teacher>(request.TeacherCreateDto);
                await _unitOfWork.TeacherRepository.Create(mappedTeacher);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                var mappedUser = _mapper.Map<UserGetDto>(appUserResult.Data);
                return Result<UserGetDto>.Success(mappedUser);
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
