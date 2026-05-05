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
    public class ProductionOrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<ProductionOrderDetail>
    {
        public void Configure(EntityTypeBuilder<ProductionOrderDetail> builder)
        {
            builder.HasKey(x => x.ProductionOrderDetailId);
            builder.Property(x => x.ProductionOrderDetailId).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.HasMany(x=>x.OrderBatches)
                .WithOne(x=>x.ProductionOrderDetail)
                .HasForeignKey(x=>x.ProductionOrderDetailId)
                .OnDelete(DeleteBehavior.Restrict); // coi lai restrict hay cascade
            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);
                
        }
    }
}
