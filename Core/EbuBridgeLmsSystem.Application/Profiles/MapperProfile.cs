using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Address;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Course;
using EbuBridgeLmsSystem.Application.Dtos.Fee;
using EbuBridgeLmsSystem.Application.Dtos.Note;
using EbuBridgeLmsSystem.Application.Dtos.Parent;
using EbuBridgeLmsSystem.Application.Dtos.Report;
using EbuBridgeLmsSystem.Application.Dtos.ReportOption;
using EbuBridgeLmsSystem.Application.Dtos.RequstToRegister;
using EbuBridgeLmsSystem.Application.Dtos.Student;
using EbuBridgeLmsSystem.Application.Dtos.Teacher;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsParent;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsTeacher;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.Login;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.SendVerificationCode;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.VerifyCode;
using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Profiles
{
    public class MapperProfile : Profile
    {
        private readonly IHttpContextAccessor _contextAccessor;




        public MapperProfile(IHttpContextAccessor contextAccessor)
        {


            _contextAccessor = contextAccessor;
            var url = _contextAccessor.HttpContext?.Request.Host.HasValue ?? false
      ? new UriBuilder(_contextAccessor.HttpContext.Request.Scheme,
                       _contextAccessor.HttpContext.Request.Host.Host,
                       _contextAccessor.HttpContext.Request.Host.Port ?? 80).Uri.AbsoluteUri
      : "https://defaulturl.com/";
            CreateMap<AppUser, UserGetDto>();
            CreateMap<TeacherCreateDto, Teacher>();
            CreateMap<ParentCreateDto, Parent>();
            CreateMap<RequstToRegisterCreateDto, RequestToRegister>();
            CreateMap<Course, CourseSelectItemDto>();
            CreateMap<Course, CourseCreateOrUpdateReturnDto>();

            CreateMap<CourseCreateDto, Course>();



            CreateMap<CourseUpdateDto, Course>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Note, NoteReturnDto>();
            CreateMap<Note, NoteListItemDto>();
            CreateMap<NoteCreateDto, Note>();
            CreateMap<NoteUpdateDto, Note>()
                            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ReportCreateDto, Report>();
            CreateMap<AppUser, UserReportReturnDto>();
            CreateMap<ReportOption, ReportOptionInReportReturnDto>();
            CreateMap<Report, ReportReturnDto>()
                               .ForMember(s => s.userReportReturnDto, map => map.MapFrom(d => d.AppUser))
                               .ForPath(s => s.optionInReportReturnDto.Id, map => map.MapFrom(d => d.ReportOption.Id))
                               .ForPath(s => s.optionInReportReturnDto.Name, map => map.MapFrom(d => d.ReportOption.Name));
            CreateMap<RequestToRegister, RequestToRegisterListItemDto>();
            CreateMap<ReportOptionCreateDto, ReportOption>();
            CreateMap<ReportOption, ReportOptionReturnDto>();
            CreateMap<Course, CourseListItemDto>();
            CreateMap<AddressCreateDto, Address>();
            CreateMap<AppUserInAdress, AppUser>();
            CreateMap<Address, AddressReturnDto>();
            CreateMap<Lesson, LessonInCourseReturnDto>();
            CreateMap<Course, CourseReturnDto>()
                .ForMember(s => s.Lessons, map => map.MapFrom(d => d.lessons));
            CreateMap<FeeCreateDto, Fee>();
            CreateMap< AppUser, AppUserInFee>();
            CreateMap<Fee, FeeListItemDto>();
            CreateMap<Fee, FeeReturnDto>();
            CreateMap<Address, AddressListItemDto>();
            CreateMap<StudentCreateDto, Student>();
            CreateMap<CreateAppUserAsStudentCommand, StudentRegistrationDto>().ReverseMap();
            CreateMap<CreateAppUserAsTeacherCommand,TeacherRegistrationDto>().ReverseMap();
            CreateMap<CreateAppUserAsParentCommand,ParentRegisterDto>().ReverseMap();
         CreateMap<VerifyCodeCommand,VerifyCodeDto>().ReverseMap();
            CreateMap<SendVerificationCodeCommand,SendVerificationCodeDto>().ReverseMap();
            CreateMap<LoginCommand,LoginDto>().ReverseMap();





        }
    }
}
