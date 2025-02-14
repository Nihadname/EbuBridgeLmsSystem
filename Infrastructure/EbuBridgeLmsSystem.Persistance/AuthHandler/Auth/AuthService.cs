using AutoMapper;
using EbuBridgeLmsSystem.Application.AppDefaults;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Student;
using EbuBridgeLmsSystem.Application.Dtos.Teacher;
using EbuBridgeLmsSystem.Application.Helpers.Extensions;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Application.Settings;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Enums;
using EbuBridgeLmsSystem.Persistance.AuthHandler.Token;
using EbuBridgeLmsSystem.Persistance.Data;
using EbuBridgeLmsSystem.Persistance.Data.Implementations;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using System.Security.Cryptography;

namespace EbuBridgeLmsSystem.Persistance.AuthHandler.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenService tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IPhotoOrVideoService _photoOrVideoService;
        private readonly ILogger _logger;
        public AuthService(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, ITokenService tokenService, ApplicationDbContext context, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IHttpContextAccessor contextAccessor, IPhotoOrVideoService photoOrVideoService, ILogger logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            this.tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _photoOrVideoService = photoOrVideoService;
            _logger = logger;
        }
        public async Task<Result<UserGetDto>> RegisterForStudent(StudentRegistrationDto studentRegistrationDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var appUserResult = await CreateUser(studentRegistrationDto.RegisterDto);
                if (!appUserResult.IsSuccess) return Result<UserGetDto>.Failure(appUserResult.ErrorKey, appUserResult.Message, appUserResult.Errors, (ErrorType)appUserResult.ErrorType);

                await _userManager.AddToRoleAsync(appUserResult.Data, RolesEnum.Student.ToString());
                studentRegistrationDto.StudentCreateDto.IsEnrolled = false;
                studentRegistrationDto.StudentCreateDto.AppUserId=appUserResult.Data.AppUserId;
                studentRegistrationDto.StudentCreateDto.AvarageScore = null;
                var mappedStudent =_mapper.Map<Student>(studentRegistrationDto.StudentCreateDto);
                await _unitOfWork.StudentRepository.Create(mappedStudent);
                await _unitOfWork.CommitTransactionAsync();

                var MappedUser = _mapper.Map<UserGetDto>(appUserResult.Data);
                return Result<UserGetDto>.Success(MappedUser);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error occurred during user registration");
                return Result<UserGetDto>.Failure("InternalServerError", "An error occurred during registration.", null, ErrorType.SystemError);
            }
            
        }
        public async Task<Result<UserGetDto>> RegisterForTeacher(TeacherRegistrationDto teacherRegistrationDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var appUserResult = await CreateUser(teacherRegistrationDto.Register);
                if (!appUserResult.IsSuccess) return Result<UserGetDto>.Failure(appUserResult.ErrorKey, appUserResult.Message, appUserResult.Errors, (ErrorType)appUserResult.ErrorType);
                await _userManager.AddToRoleAsync(appUserResult.Data, RolesEnum.Teacher.ToString());
                teacherRegistrationDto.Teacher.AppUserId = appUserResult.Data.Id;
                var MappedTeacher = _mapper.Map<Teacher>(teacherRegistrationDto.Teacher);
                await _unitOfWork.TeacherRepository.Create(MappedTeacher);
                await _unitOfWork.CommitTransactionAsync();
                var MappedUser = _mapper.Map<UserGetDto>(appUserResult.Data);
                return Result<UserGetDto>.Success(MappedUser);
            }
            catch (Exception ex)
            {
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    _logger.LogError(ex, "Error occurred during user registration");
                    return Result<UserGetDto>.Failure("InternalServerError", "An error occurred during registration.", null, ErrorType.SystemError);
                }
            }
        }

        private async Task<Result<AppUser>> CreateUser(RegisterDto registerDto)
        {
            var existUser = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existUser != null) return Result<AppUser>.Failure("UserName", "UserName is already Taken", null, ErrorType.BusinessLogicError);
            var existUserEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existUserEmail != null)
                return Result<AppUser>.Failure("Email", "Email is already taken", null, ErrorType.BusinessLogicError);
            if (await _userManager.Users.FirstOrDefaultAsync(s => s.PhoneNumber.ToLower() == registerDto.PhoneNumber.ToLower()) is not null)
            {
                return Result<AppUser>.Failure("PhoneNumber", "PhoneNumber already exists", null, ErrorType.BusinessLogicError);
            }
            if (DateTime.UtcNow.Year - registerDto.BirthDate.Year < 15)
            {
                return Result<AppUser>.Failure("BirthDate", "Student can not be younger than 15", null, ErrorType.BusinessLogicError);
            }
            AppUser appUser = new AppUser();
            appUser.UserName = registerDto.UserName;
            appUser.Email = registerDto.Email;
            appUser.fullName = registerDto.FullName;
            appUser.PhoneNumber = registerDto.PhoneNumber;
            appUser.Image = AppDefaultValue.DefaultProfileImageUrl;
            appUser.CreatedTime = DateTime.UtcNow;
            appUser.BirthDate = registerDto.BirthDate;
            appUser.IsFirstTimeLogined = true;
            appUser.IsReportedHighly = false;
            var result = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.ToDictionary(e => e.Code, e => e.Description);
                List<string> errors = new List<string>();
                foreach (KeyValuePair<string, string> keyValues in errorMessages)
                {
                    errors.Add(keyValues.Key + " " + keyValues.Value);
                }

                var response = Result<string>.Failure("User  errors found", null, errors, ErrorType.ValidationError);
            }
            var customerOptions = new Stripe.CustomerCreateOptions
            {
                Email = appUser.Email,
                Name = appUser.UserName
            };
            var service = new CustomerService();
            var stripeCustomer = await service.CreateAsync(customerOptions);
            appUser.CustomerId = stripeCustomer.Id;
            var ExistedRequestRegister = await _unitOfWork.RequstToRegisterRepository.GetEntity(s => s.Email == appUser.Email);
            if (ExistedRequestRegister != null)
            {
                string body;
                using (StreamReader sr = new StreamReader("wwwroot/templates/SendingAccountInformation.html"))
                {
                    body = sr.ReadToEnd();
                }
                body = body.Replace("{{UserName}}", appUser.UserName).Replace("{{Password}}", registerDto.Password)
                    .Replace("{{Email}}", appUser.Email);

                BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(appUser.Email, "Account details", body, true));

            }
            await SendVerificationCode(new SendVerificationCodeDto {Email= appUser.Email });
            return Result<AppUser>.Success(appUser);
        }
        public async Task<Result<string>> SendVerificationCode(SendVerificationCodeDto sendVerificationCodeDto)
        {
            var user = await _userManager.FindByEmailAsync(sendVerificationCodeDto.Email);
            if (user is null) return Result<string>.Failure("user", "user is null", null, ErrorType.NotFoundError);
            var verificationCode = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
            string salt;
            string hashedCode = verificationCode.GenerateHash(out salt);
            user.VerificationCode = hashedCode;
            user.Salt = salt;
            user.ExpiredDate = DateTime.UtcNow.AddMinutes(10);
            user.IsEmailVerificationCodeValid = false;
            await _userManager.UpdateAsync(user);
            var body = $"<h1>Welcome!</h1><p>Thank you for joining us. We're excited to have you!, this is your verfication code {verificationCode} </p>";
            BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(user.Email, "verfication code",body,true));
            return Result<string>.Success("Verification code sent");
        }
    }
}
