
using BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers;
using BatchSystem.Domain.Logins;
using BatchSystem.Domain.OrderBatchs.OrderBatchStatusHistories;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.Products;
using Domain.OrderBatchs;

namespace BatchSystem.Application.ProductionOrderDispatchers
{
    public class ProductionOrderDispatcher : IProductionOrderDispatcher
    {
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IOrderBatchStatusHistoryRepository _orderBatchStatusHistoryRepository;
        private readonly IOrderBatchCommandPublisher _orderBatchCommandPublisher;
        private readonly ILoginRepository _loginRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductionOrderDispatcher(IProductionOrderRepository productionOrderRepository, IOrderBatchStatusHistoryRepository orderBatchStatusHistoryRepository, IOrderBatchCommandPublisher orderBatchCommandPublisher, ILoginRepository loginRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productionOrderRepository=productionOrderRepository;
            _orderBatchStatusHistoryRepository=orderBatchStatusHistoryRepository;
            _orderBatchCommandPublisher=orderBatchCommandPublisher;
            _loginRepository=loginRepository;
            _productRepository=productRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task TryDispatchNextAsync(CancellationToken cancellationToken)
        {
            var hasRunningOrder = await _productionOrderRepository.HasRunningOrder();

            if (hasRunningOrder)
                return;

            var productionOrder = await _productionOrderRepository.GetNextReadyOrderTracking();

            if (productionOrder == null)
                return;

            var nextGroup = productionOrder.ProductionOrderDetails
                .OrderBy(d => d.SequenceNo)
                .Select(d => new
                {
                    Detail = d,
                    Batches = d.OrderBatches
                        .Where(b => b.Status == OrderBatchStatus.Pending)
                        .OrderBy(b => b.BatchNo)
                        .ToList()
                })
                .FirstOrDefault(x => x.Batches.Any());

            if (nextGroup == null)
            {
                Console.WriteLine("No group is ready");
                return;
            }

            foreach (var batch in nextGroup.Batches)
            {
                var history = batch.ChangeStatus(OrderBatchStatus.Sent);
                await _orderBatchStatusHistoryRepository.AddAsync(history);
            }

            await _unitOfWork.SaveEntitiesAsync(cancellationToken);
            var product = await _productRepository.GetById(nextGroup.Detail.ProductId);
            var customer = await _loginRepository.GetById(productionOrder.CustomerLoginId);
            var productCode = product.ProductCode;
            var customerCode = customer.LoginCode;

            // Tạm hard-code giống hiện tại, sau này thay bằng productCode/customerCode thật
            await _orderBatchCommandPublisher.PublishBatchGroupReadyAsync(
                nextGroup.Detail,
                nextGroup.Batches,
                productCode,
                customerCode,
                cancellationToken);
        }
    }
}
