namespace BatchSystem.Application.Commands.Prpoducts.Update
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public string ProductId { get; set;}
        public string? ProductName { get; set;}
        public string? RecipeId { get; set;}

    }
}
