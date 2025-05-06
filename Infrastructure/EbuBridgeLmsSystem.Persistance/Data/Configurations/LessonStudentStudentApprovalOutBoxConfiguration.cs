using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public class LessonStudentStudentApprovalOutBoxConfiguration : IEntityTypeConfiguration<LessonStudentStudentApprovalOutBox>
    {
        public void Configure(EntityTypeBuilder<LessonStudentStudentApprovalOutBox> builder)
        {
          builder.OwnsOne(s=>s.TeacherDetailApprovalOutBox);
        }
    }
}
