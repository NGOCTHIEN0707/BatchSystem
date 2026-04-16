using AutoMapper;
using BatchSystem.Application.Exceptions;
using BatchSystem.Application.Notifications.ProductionOrders;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.SeedWork;
using Domain.ProductionOrders;
using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders.ChangeStatus
{
    public class ChangeProductionOrderStatusToReadyCommandHandler : IRequestHandler<ChangeProductionOrderStatusToReadyCommand, bool>
    {
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ChangeProductionOrderStatusToReadyCommandHandler(IProductionOrderRepository productionOrderRepository, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _productionOrderRepository=productionOrderRepository;
            _unitOfWork=unitOfWork;
            _mediator=mediator;
        }

        public async Task<bool> Handle(ChangeProductionOrderStatusToReadyCommand request, CancellationToken cancellationToken)
        {
            var productionOrderToUpdateStatus = await _productionOrderRepository.GetById(request.ProductionOrderId);
            if (productionOrderToUpdateStatus == null) throw new EntityNotFoundException(nameof(ProductionOrder), request.ProductionOrderId.ToString());

            productionOrderToUpdateStatus.ConfirmReady();
            //var orderBatchIds = productionOrderToUpdateStatus
            //                    .ProductionOrderDetails
            //                    .SelectMany(d => d.OrderBatches)
            //                    .Select(b => b.OrderBatchId)
            //                    .ToList();
            _productionOrderRepository.UpdateAsync(productionOrderToUpdateStatus);

            await _unitOfWork.SaveEntitiesAsync(cancellationToken);
            var productionOrderConfirmedReadyNotification = new ProductionOrderConfirmedReadyNotification(
                productionOrderToUpdateStatus.ProductionOrderId
            );
            await _mediator.Publish(productionOrderConfirmedReadyNotification, cancellationToken);
            return true;
        }
    }
}
