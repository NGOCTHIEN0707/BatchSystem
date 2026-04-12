namespace BatchSystem.Application.Queries.GetAllWithRecipe
{
    public class RecipeDto
    {
        public string RecipeId { get; set; }
        public string RecipeName { get; set; }
        public List<RecipeMaterialDto> Materials { get; set; } = new();
    }
}
