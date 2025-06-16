using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations;

public sealed class CourseLanguageConfiguration: IEntityTypeConfiguration<CourseLanguage>
{
    public void Configure(EntityTypeBuilder<CourseLanguage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CourseId).IsRequired();
        builder.Property(x => x.LanguageId).IsRequired();
    }
}