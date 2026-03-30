using Domain.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class RecipeMaterialEntityTypeConfiguration : IEntityTypeConfiguration<RecipeMaterial>
    {
        public void Configure(EntityTypeBuilder<RecipeMaterial> builder)
        {
            throw new NotImplementedException();
        }
    }
}
