using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public sealed class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(s => s.CreatedTime);
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s=>s.Title).HasMaxLength(50);
            builder.Property(s=> s.Description).HasMaxLength(250);
            builder.Property(s=>s.CategoryName).HasMaxLength(90);
            builder.HasOne(s=>s.AppUser).WithMany(a => a.Notes)
        .OnDelete(DeleteBehavior.Cascade).HasForeignKey(s=>s.AppUserId);
            builder.HasIndex(s => s.CreatedTime);
        }
    }
}
