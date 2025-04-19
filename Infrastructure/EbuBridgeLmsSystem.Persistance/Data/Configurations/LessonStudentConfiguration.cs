using EbuBridgeLmsSystem.Domain.Entities;
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
    public class LessonStudentConfiguration : IEntityTypeConfiguration<LessonStudent>
    {
        public void Configure(EntityTypeBuilder<LessonStudent> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasKey(e => e.Id);
            builder.HasIndex(s => s.CreatedTime);
            builder.HasKey(ls => new { ls.LessonId, ls.StudentId });
            builder.HasOne(ls => ls.Lesson)
        .WithMany(l => l.LessonStudents)
        .HasForeignKey(ls => ls.LessonId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(ls => ls.Student)
        .WithMany(s => s.lessonStudents)
        .HasForeignKey(ls => ls.StudentId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(s => s.isFinished).HasDefaultValue(false);
        }
    }
}
