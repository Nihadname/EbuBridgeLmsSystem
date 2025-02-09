﻿using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Persistance.IdentityEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<IdentityEntity.AppUser>
    {
        public void Configure(EntityTypeBuilder<IdentityEntity.AppUser> builder)
        {
            builder.Property(s => s.UserName).HasMaxLength(100).IsRequired(true);
            builder.Property(s => s.Email).IsRequired()
                       .HasMaxLength(255)
                       .HasAnnotation("RegularExpression",
                                      @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            builder.HasOne(a => a.DomainUser)
            .WithOne()
            .HasForeignKey<AppUser>(u => u.AppUserId);
        }
    }
}
