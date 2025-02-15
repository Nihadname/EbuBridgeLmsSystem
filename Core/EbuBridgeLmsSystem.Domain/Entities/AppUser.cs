using LearningManagementSystem.Core.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
        public string fullName { get; set; }
        public string? Image { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? BlockedUntil { get; set; }
        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                Age = CalculateAgeOfUser(_birthDate);
            }
        }

        public int Age { get; private set; }
        public Address Address { get; set; }
        public void UpdateAge()
        {
            Age = CalculateAgeOfUser(BirthDate);
        }

        private int CalculateAgeOfUser(DateTime birthDate)
        {
            var Now = DateTime.Now;
            int age = Now.Year - birthDate.Year;

            if (Now.Month < birthDate.Month || (Now.Month == birthDate.Month && Now.Day < birthDate.Day))
                age--;

            return age;
        }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
        public Parent Parent { get; set; }
        public string   AppUserId   { get; set; }
        public ICollection<Note> Notes { get; set; }
        public ICollection<Report> Reports { get; set; }
        public bool IsFirstTimeLogined { get; set; }
        public bool IsReportedHighly { get; set; }
        public bool IsEmailVerificationCodeValid { get; set; }
        public string VerificationCode { get; set; }
        public string Salt { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string CustomerId { get; set; }

       
    }
}
