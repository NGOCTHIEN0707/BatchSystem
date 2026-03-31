using Domain.ProductionOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class ProductionOrderEntityTypeConfiguration : IEntityTypeConfiguration<ProductionOrder>
    {
        public void Configure(EntityTypeBuilder<ProductionOrder> builder)
        {
            builder.HasKey(x => x.ProductionOrderId);
            builder.Property(x => x.ProductionOrderId).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.HasMany(x=>x.ProductionOrderDetails)
                .WithOne(x=>x.ProductionOrder)
                .HasForeignKey(x=>x.ProductionOrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
