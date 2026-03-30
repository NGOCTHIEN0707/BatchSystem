using Domain.Stations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class StationTypeEntityTypeConfiguration : IEntityTypeConfiguration<StationType>
    {
        public void Configure(EntityTypeBuilder<StationType> builder)
        {
            builder.HasKey(x => x.StationTypeId);
            builder.Property(x => x.StationTypeId).HasDefaultValueSql("NEWID()");
            builder.HasMany(x=>x.Stations)
                .WithOne(x=>x.StationType)
                .HasForeignKey(x=>x.StationTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
