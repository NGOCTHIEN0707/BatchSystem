using Domain.Alarms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class AlarmEventEntityTypeConfiguration : IEntityTypeConfiguration<AlarmEvent>
    {
        public void Configure(EntityTypeBuilder<AlarmEvent> builder)
        {
            throw new NotImplementedException();
        }
    }
}
