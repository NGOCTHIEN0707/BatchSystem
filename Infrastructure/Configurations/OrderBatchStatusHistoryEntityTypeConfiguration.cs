using BatchSystem.Domain.OrderBatchs.OrderBatchStatusHistories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Configurations
{
    public class OrderBatchStatusHistoryEntityTypeConfiguration : IEntityTypeConfiguration<OrderBatchStatusHistory>
    {
        public void Configure(EntityTypeBuilder<OrderBatchStatusHistory> builder)
        {
            builder.HasKey(x => x.OrderBatchStatusHistoryId);
            builder.Property(x => x.OrderBatchStatusHistoryId)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();
            builder.HasOne(x => x.OrderBatch)
                .WithMany()
                .HasForeignKey(x => x.OrderBatchId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.ChangedAt).IsRequired();
            builder.Property(x => x.Status).IsRequired();


        }
    }
}
