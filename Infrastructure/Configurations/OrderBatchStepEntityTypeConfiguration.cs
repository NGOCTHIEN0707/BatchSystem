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
            builder.Property(x => x.OrderBatchStepId).HasDefaultValueSql("NEWID()");
        }
    }
}
