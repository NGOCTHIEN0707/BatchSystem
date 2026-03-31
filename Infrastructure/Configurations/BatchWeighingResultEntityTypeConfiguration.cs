using Domain.OrderBatchs.BatchWeightResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class BatchWeighingResultEntityTypeConfiguration : IEntityTypeConfiguration<BatchWeighingResult>
    {
        public void Configure(EntityTypeBuilder<BatchWeighingResult> builder)
        {
            builder.HasKey(x => x.BatchWeighingResultId);
            builder.Property(x => x.BatchWeighingResultId).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.HasOne(x=>x.Material)
                .WithMany()
                .HasForeignKey(x=>x.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => x.MaterialId);
            builder.HasIndex(x => new { x.OrderBatchId, x.CapturedAt });
        }
    }
}
