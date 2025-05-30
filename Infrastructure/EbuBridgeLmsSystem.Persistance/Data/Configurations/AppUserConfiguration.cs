﻿using EbuBridgeLmsSystem.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UserName).HasMaxLength(100).IsRequired(true);
            builder.Property(s => s.Email).IsRequired()
                       .HasMaxLength(255)
                       .HasAnnotation("RegularExpression",
                                      @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            builder.Property(s=>s.IsDeleted).HasDefaultValue(false);

            
        }
    }
}
