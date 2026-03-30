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
            throw new NotImplementedException();
        }
    }
}
