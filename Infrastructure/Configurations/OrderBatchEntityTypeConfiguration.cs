using Domain.OrderBatchs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class OrderBatchEntityTypeConfiguration : IEntityTypeConfiguration<OrderBatch>
    {
        public void Configure(EntityTypeBuilder<OrderBatch> builder)
        {
            builder.HasKey(x => x.OrderBatchId);
            builder.Property(x => x.OrderBatchId).HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasMany(x=>x.BatchWeighingResults)
                .WithOne(x=>x.OrderBatch)
                .HasForeignKey(x=>x.OrderBatchId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.ProductionOrderId);
        }
    }
}
