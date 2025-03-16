using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
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


namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent
{
    public class CreateAppUserAsStudentHandler : IRequestHandler<CreateAppUserAsStudentCommand,Result<UserGetDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<CreateAppUserAsStudentHandler> _logger;
        private readonly IValidator<RegisterDto> _validator;
        private readonly IBackgroundJobClient _backgroundJobClient;
        public CreateAppUserAsStudentHandler(IMapper mapper, UserManager<AppUser> userManager, IEmailService emailService, IUnitOfWork unitOfWork, ILogger<CreateAppUserAsStudentHandler> logger, IValidator<RegisterDto> validator, IBackgroundJobClient backgroundJobClient)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _validator = validator;
            _backgroundJobClient = backgroundJobClient;
        }

        async Task<Result<UserGetDto>> IRequestHandler<CreateAppUserAsStudentCommand, Result<UserGetDto>>.Handle(CreateAppUserAsStudentCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var validationResult = await _validator.ValidateAsync(request.RegisterDto, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return Result<UserGetDto>.Failure(null, validationResult.Errors.Select(e => e.ErrorMessage).ToList(),ErrorType.ValidationError);
                }

                var appUserResult = await _userManager.CreateUser(request.RegisterDto, _unitOfWork, _emailService, _backgroundJobClient);
                if (!appUserResult.IsSuccess)
                    return Result<UserGetDto>.Failure(appUserResult.Error, appUserResult.Errors, (ErrorType)appUserResult.ErrorType);
                await _userManager.AddToRoleAsync(appUserResult.Data, RolesEnum.Student.ToString());
                request.StudentCreateDto.AppUserId = appUserResult.Data.Id;
                request.StudentCreateDto.IsEnrolled = false;
                request.StudentCreateDto.AvarageScore = null;
                var mappedStudent = _mapper.Map<Student>(request.StudentCreateDto);
                await _unitOfWork.StudentRepository.Create(mappedStudent);
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
