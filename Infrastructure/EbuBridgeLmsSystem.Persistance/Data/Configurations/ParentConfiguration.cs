using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasOne(s=>s.AppUser).WithOne(a => a.Parent)
        .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(s => s.CreatedTime);
        }
    }
}
