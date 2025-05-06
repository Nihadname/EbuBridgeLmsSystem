using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
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

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsParent
{
    public sealed class CreateAppUserAsParentHandler : IRequestHandler<CreateAppUserAsParentCommand, Result<UserGetDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateAppUserAsParentHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobClient _backgroundJobClient;
        public CreateAppUserAsParentHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IEmailService emailService, IMapper mapper, IBackgroundJobClient backgroundJobClient, ILogger<CreateAppUserAsParentHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailService = emailService;
            _mapper = mapper;

            _backgroundJobClient = backgroundJobClient;
            _logger = logger;
        }

        public async Task<Result<UserGetDto>> Handle(CreateAppUserAsParentCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
               
                var appUserResult = await _userManager.CreateUser(request.RegisterDto, _unitOfWork, _emailService, _backgroundJobClient);
                if(!appUserResult.IsSuccess)
                    return Result<UserGetDto>.Failure(appUserResult.Error, appUserResult.Errors, (ErrorType)appUserResult.ErrorType);
                await _userManager.AddToRoleAsync(appUserResult.Data, RolesEnum.Parent.ToString());
                request.ParentCreateDto.AppUserId = appUserResult.Data.Id   ;
                var mappedParent = _mapper.Map<Parent>(request.ParentCreateDto);
                var Students = new List<Student>();
                if (request.ParentCreateDto.StudentIds.Any())
                {
                    foreach (Guid student in request.ParentCreateDto.StudentIds)
                    {
                        if (await _unitOfWork.StudentRepository.isExists(s => s.Id == student) is not false)
                        {
                            var existedStudent = await _unitOfWork.StudentRepository.GetEntity(s => s.Id == student && !s.IsDeleted);
                            if (existedStudent is null)
                            {
                                return Result<UserGetDto>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
                            }
                            Students.Add(existedStudent);
                        }
                        else
                        {
                            return Result<UserGetDto>.Failure(Error.Custom("StudentId", "the choosen student  doesnt exist"), null, ErrorType.NotFoundError);
                        }
                    }
                    mappedParent.Students = Students;
                }
                await _unitOfWork.ParentRepository.Create(mappedParent);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                var MappedUser = _mapper.Map<UserGetDto>(appUserResult.Data);
                return Result<UserGetDto>.Success(MappedUser,SuccessReturnType.Created);

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
