using Domain.Alarms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class AlarmEventEntityTypeConfiguration : IEntityTypeConfiguration<AlarmEvent>
    {
        public void Configure(EntityTypeBuilder<AlarmEvent> builder)
        {
            builder.HasKey(x => x.AlarmEventId);
            builder.Property(x => x.AlarmEventId).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.HasOne(x=>x.AlarmDefinition)
                .WithMany()
                .HasForeignKey(x => x.AlarmDefinitionId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.ProductionOrder)
                .WithMany()
                .HasForeignKey(x=>x.ProductionOrderId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x=>x.OrderBatch)
                .WithMany()
                .HasForeignKey(x=>x.OrderBatchId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasIndex(x => new { x.OrderBatchId, x.OccurredAt });
            builder.HasIndex(x => x.ProductionOrderId);
        }
    }
}
