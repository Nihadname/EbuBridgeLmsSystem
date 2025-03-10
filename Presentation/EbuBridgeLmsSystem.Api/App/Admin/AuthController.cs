﻿using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Parent;
using EbuBridgeLmsSystem.Application.Dtos.Teacher;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsParent;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsTeacher;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.App.Admin
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly  IMediator _mediator;
        private readonly IMapper _mapper;
        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
            var mappedRegisterDto = _mapper.Map<CreateAppUserAsTeacherCommand>(teacherRegistrationDto);
            var result=await _mediator.Send(mappedRegisterDto);
            return this.ToActionResult(result);
        }
        [HttpPost("RegisterForParent")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterForParent(ParentRegisterDto parentRegisterDto)
        {
            var mappedRegisterDto = _mapper.Map<CreateAppUserAsParentCommand>(parentRegisterDto);
            var result = await _mediator.Send(mappedRegisterDto);
            return this.ToActionResult(result);
        }
       
      

    }
}
