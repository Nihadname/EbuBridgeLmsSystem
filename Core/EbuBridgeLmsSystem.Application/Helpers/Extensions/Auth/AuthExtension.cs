using EbuBridgeLmsSystem.Application.AppDefaults;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Security.Cryptography;

namespace EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth
{
    public static class AuthExtension
    {

        public static async Task<Result<AppUser>> CreateUser(this UserManager<AppUser> userManager,
            RegisterDto registerDto,
            IUnitOfWork unitOfWork,
            IEmailService emailService
            )
        {
            var existUser = await userManager.FindByNameAsync(registerDto.UserName);
            if (existUser != null) return Result<AppUser>.Failure("UserName", "UserName is already Taken", null, ErrorType.BusinessLogicError);
            var existUserEmail = await userManager.FindByEmailAsync(registerDto.Email);
            if (existUserEmail != null)
                return Result<AppUser>.Failure("Email", "Email is already taken", null, ErrorType.BusinessLogicError);
            if (await userManager.Users.FirstOrDefaultAsync(s => s.PhoneNumber.ToLower() == registerDto.PhoneNumber.ToLower()) is not null)
            {
                return Result<AppUser>.Failure("PhoneNumber", "PhoneNumber already exists", null, ErrorType.BusinessLogicError);
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
            var result = await userManager.CreateAsync(appUser, registerDto.Password);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.ToDictionary(e => e.Code, e => e.Description);
                List<string> errors = new List<string>();
                foreach (KeyValuePair<string, string> keyValues in errorMessages)
                {
                    errors.Add(keyValues.Key + " " + keyValues.Value);
                }

                var response = Result<AppUser>.Failure("User  errors found", null, errors, ErrorType.ValidationError);
                return response;
            }
            var customerOptions = new CustomerCreateOptions
            {
                Email = appUser.Email,
                Name = appUser.UserName
            };
            var service = new CustomerService();
            var stripeCustomer = await service.CreateAsync(customerOptions);
            appUser.CustomerId = stripeCustomer.Id;
            var ExistedRequestRegister = await unitOfWork.RequstToRegisterRepository.GetEntity(s => s.Email == appUser.Email);
            if (ExistedRequestRegister != null)
            {
                string body;
                using (StreamReader sr = new StreamReader("wwwroot/templates/SendingAccountInformation.html"))
                {
                    body = sr.ReadToEnd();
                }
                body = body.Replace("{{UserName}}", appUser.UserName).Replace("{{Password}}", registerDto.Password)
                    .Replace("{{Email}}", appUser.Email);

                BackgroundJob.Enqueue(() => emailService.SendEmailAsync(appUser.Email, "Account details", body, true));

            }
            var sendVerificationCodeResult = await userManager.SendVerificationCode(new SendVerificationCodeDto { Email = appUser.Email }, emailService);
            if (!sendVerificationCodeResult.IsSuccess)
              return  Result<AppUser>.Failure(sendVerificationCodeResult.ErrorKey, sendVerificationCodeResult.Message, sendVerificationCodeResult.Errors, (ErrorType)sendVerificationCodeResult.ErrorType);
            return Result<AppUser>.Success(appUser);
        }
        public static async Task<Result<string>> SendVerificationCode(this UserManager<AppUser> userManager,
            SendVerificationCodeDto sendVerificationCodeDto,
            IEmailService emailService)
        {
            var user = await userManager.FindByEmailAsync(sendVerificationCodeDto.Email);
            if (user is null) return Result<string>.Failure("user", "user is null", null, ErrorType.NotFoundError);
            var verificationCode = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
            string salt;
            string hashedCode = verificationCode.GenerateHash(out salt);
            user.VerificationCode = hashedCode;
            user.Salt = salt;
            user.ExpiredDate = DateTime.UtcNow.AddMinutes(10);
            user.IsEmailVerificationCodeValid = false;
            await userManager.UpdateAsync(user);
            var body = $"<h1>Welcome!</h1><p>Thank you for joining us. We're excited to have you!, this is your verfication code {verificationCode} </p>";
            BackgroundJob.Enqueue(() => emailService.SendEmailAsync(user.Email, "verfication code", body, true));
            return Result<string>.Success("Verification code sent");
        }
        public static async Task<Result<AppUser>> GetUserByEmailAsync(this UserManager<AppUser> userManager,string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result<AppUser>.Failure(null, "User not found.", null, ErrorType.NotFoundError);
            }
            return Result<AppUser>.Success(user);
        }

    }
}
