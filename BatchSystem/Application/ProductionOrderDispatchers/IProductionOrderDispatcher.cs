namespace BatchSystem.Application.ProductionOrderDispatchers
{
    public interface IProductionOrderDispatcher
    {
        Task TryDispatchNextAsync(CancellationToken cancellationToken);
    }
}
