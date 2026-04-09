namespace BatchSystem.Application.Commands.Recipes.Create
{
    public class CreateRecipeCommand : IRequest<bool>
    {
        public string RecipeName { get; set; }
        public List<CreateRecipeMaterialDto> RecipeMaterials { get; set; } = new List<CreateRecipeMaterialDto>();
    }
    public class CreateRecipeMaterialDto
    {
        public string MaterialId { get; set; }
        public decimal TargetKg { get; set; }
        public decimal ToleranceMinKg { get; set; }
        public decimal ToleranceMaxKg { get; set; }
    }
}
