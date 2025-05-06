using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
   
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(s => s.fullName).HasMaxLength(150).IsRequired(true);
            builder.Property(s => s.VerificationCode);
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.IsBlocked).HasDefaultValue(false);
            builder.Property(s => s.BirthDate).IsRequired(true);
            builder.HasCheckConstraint("CK_User_MinimumAge", "DATEDIFF(YEAR, BirthDate, GETDATE()) >= 15");
            builder.HasMany(s => s.Reports).WithOne(s => s.AppUser).HasForeignKey(s => s.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(s=>s.Address).WithOne(s=>s.AppUser).HasForeignKey<Address>(s=>s.AppUserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s=>s.Parent).WithOne(s=>s.AppUser).HasForeignKey<Parent>(s=>s.AppUserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s=>s.Teacher).WithOne(s=>s.AppUser).HasForeignKey<Teacher>(s=>s.AppUserId).OnDelete(DeleteBehavior.Cascade);    
            builder.HasOne(s=>s.Student).WithOne(s=>s.AppUser).HasForeignKey<Student>(s=>s.AppUserId).OnDelete(DeleteBehavior.Cascade);
                    }
    }
}
