﻿using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasKey(e => e.Id);
            builder.Property(s => s.AvarageScore).HasColumnType("decimal(18, 2)");
            builder
       .HasOne(s => s.Parent)
       .WithMany(p => p.Students);
     builder.HasOne(s => s.AppUser).WithOne(p => p.Student).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.IsEnrolledInAnyCourse).HasDefaultValue(false);
        }
    }
}
