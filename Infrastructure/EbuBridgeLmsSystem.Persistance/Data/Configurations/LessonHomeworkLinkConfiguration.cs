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
    public sealed class LessonHomeworkLinkConfiguration : IEntityTypeConfiguration<LessonHomeworkLink>
    {
        public void Configure(EntityTypeBuilder<LessonHomeworkLink> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.Name).HasMaxLength(200);
            builder.Property(s => s.Description).HasMaxLength(200);
        }
    }
}
