﻿using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class LessonUnitAssignmentConfiguration : IEntityTypeConfiguration<LessonUnitAssignment>
    {
        public void Configure(EntityTypeBuilder<LessonUnitAssignment> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasKey(e => e.Id);
            builder.OwnsOne(e => e.LessonMeeting, meeting =>
            {
                meeting.Property(m => m.Link).HasColumnName("MeetingLink");
                meeting.Property(m => m.IsVerified).HasColumnName("MeetingIsVerified");
               
            });
        }
    }
}
