using AutoMapper;
using EbuBridgeLmsSystem.Api.App.Common;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Parent;
using EbuBridgeLmsSystem.Application.Dtos.Teacher;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsParent;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsTeacher;
using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.App.Admin
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AuthController : BaseAdminController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<TeacherCreateDto> _teacherCreateValidator;
        public AuthController(ISender mediator, IMapper mapper, IValidator<RegisterDto> registerValidator, IValidator<TeacherCreateDto> teacherCreateValidator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _registerValidator = registerValidator;
            _teacherCreateValidator = teacherCreateValidator;
        }
        //[HttpPost("RegisterForStudent")]
        //[Authorize(Roles ="Admin")]
        //public async Task<IActionResult> RegisterForStudent(StudentRegistrationDto studentRegistrationDto)
        //{
        //    var mappedRegisterDto=_mapper.Map<CreateAppUserAsStudentCommand>(studentRegistrationDto);
        //    var result = await _mediator.Send(mappedRegisterDto);
        //   return this.ToActionResult(result);
        //}
        [HttpPost("RegisterForTeacher")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterForTeacher(TeacherRegistrationDto teacherRegistrationDto)
        {
            var registerValidatorResult =await _registerValidator.ValidateAsync(teacherRegistrationDto.RegisterDto);
            var teacherValidatorResult=await _teacherCreateValidator.ValidateAsync(teacherRegistrationDto.TeacherCreateDto);
            List<FluentValidation.Results.ValidationResult> validations = new();
            validations.Add(registerValidatorResult);
            validations.Add(teacherValidatorResult);
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
                var returnedResult= Result<Unit>.Failure(null, errors, ErrorType.ValidationError);
                return this.ToActionResult(returnedResult);
            }
            var mappedRegisterDto = _mapper.Map<CreateAppUserAsTeacherCommand>(teacherRegistrationDto);
            var result=await _mediator.Send(mappedRegisterDto);
            return this.ToActionResult(result);
        }
        [HttpPost("RegisterForParent")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterForParent(ParentRegisterDto parentRegisterDto)
        {
            var registerDtoValidator = await _registerValidator.ValidateAsync(parentRegisterDto.Register);
            if (!registerDtoValidator.IsValid)
            {
                var returnedResult = Result<Unit>.Failure(null, registerDtoValidator.Errors.Select(e => e.ErrorMessage).ToList(), ErrorType.ValidationError);
                return this.ToActionResult(returnedResult);
            }
            var mappedRegisterDto = _mapper.Map<CreateAppUserAsParentCommand>(parentRegisterDto);
            var result = await _mediator.Send(mappedRegisterDto);
            return this.ToActionResult(result);
        }
       
      

    }
}
