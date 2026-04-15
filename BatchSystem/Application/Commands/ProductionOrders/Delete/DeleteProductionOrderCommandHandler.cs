using BatchSystem.Application.Exceptions;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.SeedWork;
using Domain.ProductionOrders;
using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders.Delete
{
    public class DeleteProductionOrderCommandHandler : IRequestHandler<DeleteProductionOrderCommand, bool>
    {
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductionOrderCommandHandler(IProductionOrderRepository productionOrderRepository, IUnitOfWork unitOfWork)
        {
            _productionOrderRepository=productionOrderRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductionOrderCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.ProductionOrderId, out var productionOrderId))
                throw new EntityNotFoundException(nameof(ProductionOrder), request.ProductionOrderId);
            var productionOrdertoDelete = await _productionOrderRepository.GetById(productionOrderId);
            if (productionOrdertoDelete == null) throw new EntityNotFoundException(nameof(ProductionOrder), request.ProductionOrderId.ToString());
            if(productionOrdertoDelete.Status != ProductionOrderStatus.Pending && productionOrdertoDelete.Status != ProductionOrderStatus.Cancelled 
                && productionOrdertoDelete.Status != ProductionOrderStatus.Ready)
            {
                throw new BusinessRuleException($"The order is {productionOrdertoDelete.Status}");
            }
            _productionOrderRepository.Delete(productionOrdertoDelete);

            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
