using Domain.ProductionOrders;
using Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products
{
    public class Product
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public bool IsActive { get; private set; } = true;
        public string RecipeId { get; private set; }
        public Recipe Recipe { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Product()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Product(string productName,string recipeId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ProductName=productName;
            RecipeId = recipeId;
        }
        public void UpdateProductName (string  productName) => ProductName = productName;
        public void UpdateRecipeId (string recipeId) => RecipeId = recipeId;
        public void DeactivateProduct()
        {
            IsActive = false;
        }
    }

}
