using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(s => s.fullName).HasMaxLength(150).IsRequired(true);
            builder.Property(s => s.VerificationCode).HasMaxLength(6);
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.IsBlocked).HasDefaultValue(false);
            builder.Property(s => s.BirthDate).IsRequired(true);
            builder.HasCheckConstraint("CK_User_MinimumAge", "DATEDIFF(YEAR, BirthDate, GETDATE()) >= 15");
            builder.HasMany(s => s.Reports).WithOne(s => s.User).HasForeignKey(s => s.UserId);

        }
    }
}
