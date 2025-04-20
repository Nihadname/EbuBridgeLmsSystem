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
    public sealed class LessonUnitStudentHomeworkSubmissionConfiguration : IEntityTypeConfiguration<LessonUnitStudentHomeworkSubmission>
    {
        public void Configure(EntityTypeBuilder<LessonUnitStudentHomeworkSubmission> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasKey(e => e.Id);
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s=>s.Content).HasMaxLength(1000);
            builder.Property(s=>s.Feedback).HasMaxLength(300);
          
        }
    }
}
