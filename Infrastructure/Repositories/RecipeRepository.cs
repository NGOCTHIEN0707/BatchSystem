using BatchSystem.Domain.Recipes;
using BatchSystem.Infrastructure.Repositories.Common;
using Domain.Recipes;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class RecipeRepository : BaseRepository, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
        }

        public void Delete(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
        }

        public async Task<Recipe?> GetById(string recipeId)
        {
            var recipe = await _context.Recipes.AsNoTracking()
                .Include(x => x.RecipeMaterials)
                .ThenInclude(x => x.Material)
                .FirstOrDefaultAsync(x => x.RecipeId == recipeId);

            return recipe;
        }

        public async Task<bool> IsRecipeNameExisted(string recipeName)
        {
            var isRecipeNameExisted = await _context.Recipes.AnyAsync(x=>x.RecipeName == recipeName);
            return isRecipeNameExisted;
        }

        public void UpdateAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
        }
    }
}
