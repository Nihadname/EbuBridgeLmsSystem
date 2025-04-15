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
    public class LessonUnitHomeworkSubmissionConfiguration : IEntityTypeConfiguration<LessonUnitHomeworkSubmission>
    {
        public void Configure(EntityTypeBuilder<LessonUnitHomeworkSubmission> builder)
        {
            builder.Property(s => s.Grade).HasColumnType("decimal(18, 2)");
        }
    }
}
