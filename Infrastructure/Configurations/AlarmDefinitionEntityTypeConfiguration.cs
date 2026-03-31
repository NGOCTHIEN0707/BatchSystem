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
    public class AlarmDefinitionEntityTypeConfiguration : IEntityTypeConfiguration<AlarmDefinition>
    {
        public void Configure(EntityTypeBuilder<AlarmDefinition> builder)
        {
            builder.HasKey(x => x.AlarmDefinitionId);
            builder.Property(x => x.AlarmDefinitionId).HasDefaultValueSql("NEWID()");
           
        }
    }
}
