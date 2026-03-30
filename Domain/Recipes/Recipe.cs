using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Recipes
{
    public class Recipe
    {
        public string RecipeId { get; private set; }
        public string RecipeName { get; private set; }
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public bool IsActive { get; private set; } = true;
        public string ProductId { get; private set; }
        public Product Product { get; private set; }
        public List<RecipeMaterial> RecipeMaterials { get; private set; } = new List<RecipeMaterial>();


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Recipe()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Recipe(string recipeName, DateTime createdDate)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            RecipeName = recipeName;
            CreatedDate = createdDate;
        }
    }
}
