namespace BatchSystem.Application.Commands.Products.Create
{
    public class CreateProductCommand : IRequest<bool>
    {
        public string? ProductName { get;  set; }
        public string RecipeId { get;  set; }
        public int weightPerPieceKg { get; set; }
    }
}
