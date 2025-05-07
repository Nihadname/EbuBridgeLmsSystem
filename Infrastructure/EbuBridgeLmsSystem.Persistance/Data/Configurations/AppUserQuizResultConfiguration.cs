using EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class AppUserQuizResultConfiguration : IEntityTypeConfiguration<AppUserQuizResult>
    {
        public void Configure(EntityTypeBuilder<AppUserQuizResult> builder)
        {
            builder.Property(s => s.Grade).HasColumnType("decimal(18, 2)");
        }
    }
}
