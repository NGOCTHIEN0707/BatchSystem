using Domain.Lines;
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
    public class StationCurrentStatusEntityTypeConfiguration : IEntityTypeConfiguration<StationCurrentStatus>
    {
        public void Configure(EntityTypeBuilder<StationCurrentStatus> builder)
        {
            builder.HasKey(x => x.StationCurrentStatusId);
            builder.Property(x => x.StationCurrentStatusId).HasDefaultValueSql("NEWID()");
            builder.HasOne(x => x.Station)
                .WithOne(x => x.CurrentStatus).
                HasForeignKey<StationCurrentStatus>(x => x.StationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
