using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public class FeeConfiguration : IEntityTypeConfiguration<Fee>
    {
        public void Configure(EntityTypeBuilder<Fee> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasKey(e => e.Id);
            builder.HasIndex(s => s.CreatedTime);
            builder.HasIndex(s => s.PaidDate);
            builder.Property(s => s.Amount).HasColumnType("decimal(18, 2)");
            builder.Property(s => s.DiscountPercentage).HasColumnType("decimal(18, 2)");
            builder.Property(s => s.DiscountedPrice).HasColumnType("decimal(18, 2)");
        }
    }
}
