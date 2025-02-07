using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.HasMany(s => s.Reports).WithOne(s => s.User).HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(s=>s.Address).WithOne(s=>s.User).HasForeignKey<Address>(s=>s.UserId);
            builder.HasOne(s=>s.Parent).WithOne(s=>s.User).HasForeignKey<Parent>(s=>s.UserId);
            builder.HasOne(s=>s.Teacher).WithOne(s=>s.User).HasForeignKey<Teacher>(s=>s.UserId);    
            builder.HasOne(s=>s.Student).WithOne(s=>s.User).HasForeignKey<Student>(s=>s.UserId);

        }
    }
}
