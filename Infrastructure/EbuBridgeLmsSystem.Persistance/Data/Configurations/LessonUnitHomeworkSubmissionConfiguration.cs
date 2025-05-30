﻿using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class LessonUnitHomeworkSubmissionConfiguration : IEntityTypeConfiguration<LessonUnitStudentHomeworkSubmission>
    {
        public void Configure(EntityTypeBuilder<LessonUnitStudentHomeworkSubmission> builder)
        {
            builder.Property(s => s.Grade).HasColumnType("decimal(18, 2)");
        }
    }
}
