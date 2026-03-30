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
    public class RecipeEntityTypeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(x => x.RecipeId);
            builder.Property(x => x.RecipeId).HasDefaultValueSql("NEWID()");
            builder.HasMany(x=>x.RecipeMaterials)
                .WithOne(x=>x.Recipe)
                .HasForeignKey(x=>x.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
