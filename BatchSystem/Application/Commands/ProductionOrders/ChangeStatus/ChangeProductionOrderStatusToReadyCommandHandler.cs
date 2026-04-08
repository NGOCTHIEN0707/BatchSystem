using BatchSystem.Application.Exceptions;
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

        public ChangeProductionOrderStatusToReadyCommandHandler(IProductionOrderRepository productionOrderRepository, IUnitOfWork unitOfWork)
        {
            _productionOrderRepository=productionOrderRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(ChangeProductionOrderStatusToReadyCommand request, CancellationToken cancellationToken)
        {
            var productionOrderToUpdateStatus = await _productionOrderRepository.GetById(request.ProductionOrderId);
            if (productionOrderToUpdateStatus == null) throw new EntityNotFoundException(nameof(ProductionOrder), request.ProductionOrderId.ToString());

            productionOrderToUpdateStatus.ConfirmReady();
            _productionOrderRepository.UpdateAsync(productionOrderToUpdateStatus);

            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
