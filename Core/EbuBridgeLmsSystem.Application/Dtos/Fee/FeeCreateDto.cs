using EbuBridgeLmsSystem.Domain.Entities;

namespace EbuBridgeLmsSystem.Application.Dtos.Fee
{
    public class FeeCreateDto
    {
        public decimal Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public Guid StudentId { get; set; }
    }
}
