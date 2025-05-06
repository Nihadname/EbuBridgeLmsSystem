using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class LessonUnitMaterialConfiguration : IEntityTypeConfiguration<LessonUnitMaterial>
    {
        public void Configure(EntityTypeBuilder<LessonUnitMaterial> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasKey(e => e.Id);
            builder.Property(s => s.Title).HasMaxLength(80);
            builder.HasIndex(s => s.CreatedTime);
        }
    }
}
