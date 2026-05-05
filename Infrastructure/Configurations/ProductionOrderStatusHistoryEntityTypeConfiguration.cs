using BatchSystem.Domain.ProductionOrders.ProductionOrderStatusHistories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Configurations
{
    public class ProductionOrderStatusHistoryEntityTypeConfiguration : IEntityTypeConfiguration<ProductionOrderStatusHistory>
    {
        public void Configure(EntityTypeBuilder<ProductionOrderStatusHistory> builder)
        {
            builder.HasKey(x => x.ProductionOrderStatusHistoryId);

            builder.Property(x => x.ProductionOrderStatusHistoryId)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ChangedAt)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasOne(x => x.ProductionOrder)
                .WithMany()
                .HasForeignKey(x => x.ProductionOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
