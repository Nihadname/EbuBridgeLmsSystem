using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using LearningManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class LessonMaterialConfiguration : IEntityTypeConfiguration<LessonUnitMaterial>
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
