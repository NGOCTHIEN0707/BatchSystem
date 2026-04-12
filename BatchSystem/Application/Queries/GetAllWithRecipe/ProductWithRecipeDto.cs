namespace BatchSystem.Application.Queries.GetAllWithRecipe
{
    public class ProductWithRecipeDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public RecipeDto Recipe { get; set; }
    }
}
