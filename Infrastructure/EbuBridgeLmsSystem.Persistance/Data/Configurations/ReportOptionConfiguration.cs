using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public class ReportOptionConfiguration : IEntityTypeConfiguration<ReportOption>
    {
        public void Configure(EntityTypeBuilder<ReportOption> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasKey(e => e.Id);
            builder.HasIndex(s => s.CreatedTime);
        }
    }
}
