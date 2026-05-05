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
        public int GrindingTimeSeconds { get; private set; }
        public int MixingTimeSeconds { get; private set; }
        public bool IsActive { get; private set; } = true;

        public List<RecipeMaterial> RecipeMaterials { get; private set; } = new List<RecipeMaterial>();


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Recipe()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Recipe(string recipeName, DateTime createdDate,int grindingTimeSeconds, int mixingTimeSeconds)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            RecipeName = recipeName;
            CreatedDate = createdDate;
            GrindingTimeSeconds = grindingTimeSeconds;
            MixingTimeSeconds = mixingTimeSeconds;
        }
        public void UpdateGrindingTimeSeconds(int grindingTimeSeconds)
        {
            if (grindingTimeSeconds < 0)
                throw new ArgumentException("Grinding time must be >= 0.");
            if (GrindingTimeSeconds == grindingTimeSeconds)
                return;
            GrindingTimeSeconds = grindingTimeSeconds;

        }
        public void UpdateMixingTimeSeconds(int  mixingTimeSeconds)
        {
            if (mixingTimeSeconds < 0)
                throw new ArgumentException("Mixing time must be >= 0.");
            if (MixingTimeSeconds == mixingTimeSeconds)
                return;

            MixingTimeSeconds = mixingTimeSeconds;

        }
        public void Deactivate() => IsActive = false;
        public void UpdateRecipeName(string recipeName) => RecipeName = recipeName;
        public RecipeMaterial AddRecipeMaterial(string materialId, decimal targetKg, decimal toleranceMinKg, decimal toleranceMaxKg)
        {
            var recipeMaterial = new RecipeMaterial(
                RecipeId,
                materialId,
                targetKg,
                toleranceMinKg,
                toleranceMaxKg
            );

            RecipeMaterials.Add(recipeMaterial);
            return recipeMaterial;
        }
        public void SyncRecipeMaterials(List<RecipeMaterialInput> inputs)
        {
            if (inputs == null || !inputs.Any())
                throw new ArgumentException("Recipe must have at least one material.");

            var inputMaterialIds = inputs.Select(x => x.MaterialId).ToHashSet();

            var materialsToRemove = RecipeMaterials
                .Where(x => !inputMaterialIds.Contains(x.MaterialId))
                .ToList();

            foreach (var item in materialsToRemove)
            {
                RecipeMaterials.Remove(item);
            }

            foreach (var input in inputs)
            {
                var existing = RecipeMaterials.FirstOrDefault(x => x.MaterialId == input.MaterialId);

                if (existing == null)
                {
                    RecipeMaterials.Add(new RecipeMaterial(
                        RecipeId,
                        input.MaterialId,
                        input.TargetKg,
                        input.ToleranceMinKg,
                        input.ToleranceMaxKg
                    ));
                }
                else
                {
                    existing.Update(
                        input.TargetKg,
                        input.ToleranceMinKg,
                        input.ToleranceMaxKg
                    );
                }
            }
        }
    }
    public class RecipeMaterialInput
    {
        public string MaterialId { get; set; } = string.Empty;
        public decimal TargetKg { get; set; }
        public decimal ToleranceMinKg { get; set; }
        public decimal ToleranceMaxKg { get; set; }
    }
}
