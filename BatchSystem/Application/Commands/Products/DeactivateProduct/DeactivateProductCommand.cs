namespace BatchSystem.Application.Commands.Products.DeactivateProduct
{
    public class DeactivateProductCommand : IRequest<bool>
    {
        public string ProductId { get; set; }
    }
}
