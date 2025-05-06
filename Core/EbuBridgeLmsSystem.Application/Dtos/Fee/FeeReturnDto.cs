using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;

namespace EbuBridgeLmsSystem.Application.Dtos.Fee
{
    public sealed class FeeReturnDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentReference { get; set; }
        public string ProvementImageUrl { get; set; }
        public string Description { get; set; }
    }
}
