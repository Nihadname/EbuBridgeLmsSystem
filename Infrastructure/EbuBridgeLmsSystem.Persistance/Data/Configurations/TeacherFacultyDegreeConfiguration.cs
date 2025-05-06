using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class TeacherFacultyDegreeConfiguration : IEntityTypeConfiguration<TeacherFacultyDegree>
    {
        public void Configure(EntityTypeBuilder<TeacherFacultyDegree> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasKey(x => x.Id);
            builder.HasKey(tfd => new { tfd.TeacherId, tfd.FacultyId, tfd.DegreeId });
            builder.HasOne(s => s.Teacher)
            .WithMany(s => s.TeacherFacultyDegrees)
            .HasForeignKey(s => s.TeacherId);
            builder.HasOne(s => s.Degree)
           .WithMany(s => s.TeacherFacultyDegrees)
           .HasForeignKey(s => s.DegreeId);
            builder.HasOne(s => s.Faculty)
          .WithMany(s => s.TeacherFacultyDegrees)
          .HasForeignKey(s => s.FacultyId);
        }
    }
}
