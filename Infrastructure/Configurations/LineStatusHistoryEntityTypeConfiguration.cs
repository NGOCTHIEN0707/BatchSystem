using Domain.Lines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class LineStatusHistoryEntityTypeConfiguration : IEntityTypeConfiguration<LineStatusHistory>
    {
        public void Configure(EntityTypeBuilder<LineStatusHistory> builder)
        {
            builder.HasKey(x => x.LineStatusHistoryId);
            builder.Property(x => x.LineStatusHistoryId).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.HasOne(x => x.Line)
                .WithMany()
                .HasForeignKey(x => x.LineId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => x.LineId);
        }
    }
}
