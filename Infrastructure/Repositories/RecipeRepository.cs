using BatchSystem.Domain.Recipes;
using Domain.Recipes;
using Infrastructure;
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

        public Task AddAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe?> GetById(string recipeId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}
