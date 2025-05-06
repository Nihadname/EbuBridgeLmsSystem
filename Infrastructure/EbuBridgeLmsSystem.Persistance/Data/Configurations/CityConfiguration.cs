using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public  void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(s => s.Name).IsUnique();
            builder.HasMany(c => c.Addresses)
        .WithOne(a => a.City)
        .HasForeignKey(a => a.CityId)
        .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.Country).WithMany(s => s.Cities)
                   .HasForeignKey(s => s.CountryId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
