using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Core.Entities
{
    public class Fee:BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountedPrice { get; set; }
       public string Description { get; set; }
        public string PaymentReference { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public string ProvementImageUrl { get; set; }
        public bool IsBankTransferAccepted { get; set; }
    }
    public enum PaymentStatus
    {
        Paid,
        Pending,
        AboutToApproachCourseToPay
    }
    public enum PaymentMethod {
        Cash,
        CreditCard,
        BankTransfer
    }

}
