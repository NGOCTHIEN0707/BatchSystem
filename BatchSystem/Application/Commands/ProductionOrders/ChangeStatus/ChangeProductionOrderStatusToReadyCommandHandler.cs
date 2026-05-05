using AutoMapper;
using BatchSystem.Application.Exceptions;
using BatchSystem.Application.Notifications.ProductionOrders;
using BatchSystem.Application.ProductionOrderDispatchers;
using BatchSystem.Domain.Logins;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.Products;
using BatchSystem.Domain.SeedWork;
using Domain.ProductionOrders;
using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders.ChangeStatus
{
    public class ChangeProductionOrderStatusToReadyCommandHandler : IRequestHandler<ChangeProductionOrderStatusToReadyCommand, bool>
    {
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginRepository _loginRepository;
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;
        private readonly IProductionOrderDispatcher _dispatcher;

        public ChangeProductionOrderStatusToReadyCommandHandler(IProductionOrderRepository productionOrderRepository, IUnitOfWork unitOfWork, ILoginRepository loginRepository, IMediator mediator, IProductRepository productRepository, IProductionOrderDispatcher dispatcher)
        {
            _productionOrderRepository=productionOrderRepository;
            _unitOfWork=unitOfWork;
            _loginRepository=loginRepository;
            _mediator=mediator;
            _productRepository=productRepository;
            _dispatcher=dispatcher;
        }

        public async Task<bool> Handle(ChangeProductionOrderStatusToReadyCommand request, CancellationToken cancellationToken)
        {
            var detailProductCodes = new Dictionary<Guid, int>();
            var productionOrderToUpdateStatus = await _productionOrderRepository.GetByIdTracking(request.ProductionOrderId);
            if (productionOrderToUpdateStatus == null) throw new EntityNotFoundException(nameof(ProductionOrder), request.ProductionOrderId.ToString());
            var customerLogin = await _loginRepository.GetById(productionOrderToUpdateStatus.CustomerLoginId);
            if (customerLogin == null)
                throw new EntityNotFoundException(
                    "CustomerLogin",
                    productionOrderToUpdateStatus.CustomerLoginId.ToString());

            var customerCode = customerLogin.LoginCode;
            foreach (var detail in productionOrderToUpdateStatus.ProductionOrderDetails.OrderBy(x => x.SequenceNo))
            {
                var product = await _productRepository.GetById(detail.ProductId);
                if(product == null) throw new EntityNotFoundException("Product", detail.ProductId);
                detailProductCodes[detail.ProductionOrderDetailId] = product.ProductCode;

                var requestedKg = detail.NumberOfPieces * product.WeightPerPieceKg;
                var batchCount = (int)Math.Ceiling(requestedKg / 50m);

                //remainingKg = requestedKg - availableKg
                //batchCount = ceil(remainingKg / 100)

                if (batchCount > 0)
                {
                    detail.CreateOrderBatches(batchCount);
                }
            }
            productionOrderToUpdateStatus.ConfirmReady();

            _productionOrderRepository.UpdateAsync(productionOrderToUpdateStatus);
            
            await _unitOfWork.SaveEntitiesAsync(cancellationToken);
            //var productionOrderConfirmedReadyNotification = new ProductionOrderConfirmedReadyNotification(
            //    productionOrderToUpdateStatus.ProductionOrderId, detailProductCodes,customerCode
            //);
            //await _mediator.Publish(productionOrderConfirmedReadyNotification, cancellationToken);
            await _dispatcher.TryDispatchNextAsync(cancellationToken);

            return true;
        }
    }
}
