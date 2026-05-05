using BatchSystem.Domain.Logins.StaffCodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Configurations
{
    public class StaffCodeEntityTypeConfiguration : IEntityTypeConfiguration<StaffCode>
    {
        public void Configure(EntityTypeBuilder<StaffCode> builder)
        {
            builder.HasKey(x => x.StaffCodeId);
            builder.Property(x => x.StaffCodeId).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Code)
                .IsRequired();

            builder.HasIndex(x => x.Code)
                .IsUnique();

            builder.Property(x => x.Role)
                .IsRequired();

            builder.Property(x => x.IsUsed)
                .IsRequired();
        }
    }
}
