using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
    {
        public void Configure(EntityTypeBuilder<QuizResult> builder)
        {

            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasKey(e => e.Id);
            builder.Property(s => s.Score).HasColumnType("decimal(18, 2)");
            builder.HasIndex(s => s.CreatedTime);

        }
    }
}
