﻿using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Parent;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsParent
{
    public sealed class CreateAppUserAsParentHandler : IRequestHandler<CreateAppUserAsParentCommand, Result<UserGetDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateAppUserAsParentHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterDto> _validator;
        private readonly IBackgroundJobClient _backgroundJobClient;
        public CreateAppUserAsParentHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IEmailService emailService, IMapper mapper, IValidator<RegisterDto> validator, IBackgroundJobClient backgroundJobClient)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailService = emailService;
            _mapper = mapper;
            _validator = validator;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Result<UserGetDto>> Handle(CreateAppUserAsParentCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var validationResult = await _validator.ValidateAsync(request.RegisterDto, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return Result<UserGetDto>.Failure(null, validationResult.Errors.Select(e => e.ErrorMessage).ToList(), ErrorType.ValidationError);
                }
                var appUserResult = await _userManager.CreateUser(request.RegisterDto, _unitOfWork, _emailService, _backgroundJobClient);
                if(!appUserResult.IsSuccess)
                    return Result<UserGetDto>.Failure(appUserResult.Error, appUserResult.Errors, (ErrorType)appUserResult.ErrorType);
                await _userManager.AddToRoleAsync(appUserResult.Data, RolesEnum.Parent.ToString());
                request.ParentCreateDto.AppUserId = appUserResult.Data.Id   ;
                var mappedParent = _mapper.Map<Parent>(request.ParentCreateDto);
                var Students = new List<Student>();
                if (request.ParentCreateDto.StudentIds.Any())
                {
                    foreach (var student in request.ParentCreateDto.StudentIds)
                    {
                        if (await _unitOfWork.StudentRepository.isExists(s => s.Id == student) is not false)
                        {
                            var ExistedStudent = await _unitOfWork.StudentRepository.GetEntity(s => s.Id == student && !s.IsDeleted);
                            Students.Add(ExistedStudent);
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
                return Result<UserGetDto>.Success(MappedUser);

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
