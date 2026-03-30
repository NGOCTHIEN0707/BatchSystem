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
    public class LineStatusHistoryEntityTypeConfiguration : IEntityTypeConfiguration<LineStatusHistory>
    {
        public void Configure(EntityTypeBuilder<LineStatusHistory> builder)
        {
            throw new NotImplementedException();
        }
    }
}
