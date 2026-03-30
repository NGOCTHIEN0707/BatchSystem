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
    public class LineCurrentStatusEntityTypeConfiguration : IEntityTypeConfiguration<LineCurrentStatus>
    {
        public void Configure(EntityTypeBuilder<LineCurrentStatus> builder)
        {
            throw new NotImplementedException();
        }
    }
}
