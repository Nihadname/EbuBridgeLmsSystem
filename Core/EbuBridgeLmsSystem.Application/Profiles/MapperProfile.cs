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
using EbuBridgeLmsSystem.Application.Features.AppUser.Commands.CreateAppUserAsStudent;
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
            CreateMap<CreateAppUserAsStudentCommand, StudentRegistrationDto>()
                .ConstructUsing(cmd => new StudentRegistrationDto
                {
                    RegisterDto = new RegisterDto(),
                    StudentCreateDto = new StudentCreateDto()
                })
            .ForMember(s => s.RegisterDto.FullName, map => map.MapFrom(d => d.FullName))
            .ForMember(s => s.RegisterDto.UserName, map => map.MapFrom(d => d.UserName))
            .ForMember(s => s.RegisterDto.Email, map => map.MapFrom(d => d.Email))
            .ForMember(s => s.RegisterDto.PhoneNumber, map => map.MapFrom(d => d.PhoneNumber))
            .ForMember(s => s.RegisterDto.Password, map => map.MapFrom(d => d.Password))
            .ForMember(s => s.RegisterDto.RepeatPassword, map => map.MapFrom(d => d.RepeatPassword))
            .ForMember(s => s.RegisterDto.BirthDate, map => map.MapFrom(d => d.BirthDate))
            .ForMember(s => s.StudentCreateDto.AvarageScore, map => map.MapFrom(d => d.AvarageScore))
            .ForMember(s => s.StudentCreateDto.AppUserId, map => map.MapFrom(d => d.AppUserId))
            .ForMember(s => s.StudentCreateDto.IsEnrolled, map => map.MapFrom(d => d.IsEnrolled));








        }
    }
}
