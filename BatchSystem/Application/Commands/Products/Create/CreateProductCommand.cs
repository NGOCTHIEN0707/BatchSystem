namespace BatchSystem.Application.Commands.Products.Create
{
    public class CreateProductCommand : IRequest<bool>
    {
        public string? ProductName { get; private set; }
        public string RecipeId { get; private set; }
    }
}
