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
            throw new NotImplementedException();
        }
    }
}
