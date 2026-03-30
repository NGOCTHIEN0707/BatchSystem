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
    public class LineEntityTypeConfiguration : IEntityTypeConfiguration<Line>
    {
        public void Configure(EntityTypeBuilder<Line> builder)
        {
            builder.HasKey(x=>x.LineId);
            builder.Property(x => x.LineId).HasDefaultValueSql("NEWID()");
            builder.HasMany(x=>x.Stations)
                .WithOne(x=>x.Line)
                .HasForeignKey(x=>x.LineId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
