using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public class LessonUnitConfiguration : IEntityTypeConfiguration<LessonUnit>
    {
        public void Configure(EntityTypeBuilder<LessonUnit> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
        }
    }
}
