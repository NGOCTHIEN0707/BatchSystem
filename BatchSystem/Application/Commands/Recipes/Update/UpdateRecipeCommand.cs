namespace BatchSystem.Application.Commands.Recipes.Update
{
    public class UpdateRecipeCommand : IRequest<bool>
    {
        public string RecipeId { get; set; }
        public string? RecipeName { get; set; }
        public int? GrindingTimeSeconds { get;  set; }
        public int? MixingTimeSeconds { get;  set; }
        public List<UpdateRecipeMaterialDto>? RecipeMaterials { get; set; } = new List<UpdateRecipeMaterialDto>();
    }
    public class UpdateRecipeMaterialDto
    {
        public string MaterialId { get; set; }
        public decimal TargetKg { get; set; }
        public decimal ToleranceMinKg { get; set; }
        public decimal ToleranceMaxKg { get; set; }
    }
}
