using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Configurations
{
    public class RequestToRegisterConfiguration : IEntityTypeConfiguration<RequestToRegister>
    {
        public void Configure(EntityTypeBuilder<RequestToRegister> builder)
        {
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UpdatedTime).HasDefaultValueSql("GETDATE()");
            builder.HasKey(e => e.Id);
            builder.Property(s=>s.ChildAge).IsRequired(false);
            builder.Property(s=>s.IsAccepted).HasDefaultValue(false);
            builder.HasIndex(s => s.CreatedTime);
        }
    }
}
