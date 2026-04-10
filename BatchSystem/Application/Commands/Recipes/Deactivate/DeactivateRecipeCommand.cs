namespace BatchSystem.Application.Commands.Recipes.Deactivate
{
    public class DeactivateRecipeCommand : IRequest<bool>
    {
        public string RecipeId { get; set; }
    }
}
