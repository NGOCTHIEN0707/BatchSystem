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
    public class OrderBatchStepEntityTypeConfiguration : IEntityTypeConfiguration<OrderBatchStep>
    {
        public void Configure(EntityTypeBuilder<OrderBatchStep> builder)
        {
            builder.HasKey(x => x.OrderBatchStepId);
            builder.Property(x => x.OrderBatchStepId).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.HasOne(x => x.Station)
                .WithMany()
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => x.StationId);
            builder.HasIndex(x => x.OrderBatchId);

        }
    }
}
