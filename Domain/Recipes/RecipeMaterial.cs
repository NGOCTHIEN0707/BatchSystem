using Domain.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Recipes
{
    public class RecipeMaterial
    {
        public string RecipeMaterialId { get; private set; }
        public string RecipeId { get; private set; }
        public string MaterialId { get; private set; }
        public decimal TargetKg { get; private set; }
        public decimal ToleranceMinKg { get; private set; }
        public decimal ToleranceMaxKg { get; private set; }
        public Recipe Recipe { get; private set; }
        public Material Material { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public RecipeMaterial()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public RecipeMaterial(string recipeId, string materialId, decimal targetKg, decimal toleranceMinKg, decimal toleranceMaxKg)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            RecipeId = recipeId;
            MaterialId = materialId;
            TargetKg = targetKg;
            ToleranceMinKg = toleranceMinKg;
            ToleranceMaxKg = toleranceMaxKg;
        }
    }
}
