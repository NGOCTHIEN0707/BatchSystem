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
            var productionOrdertoDelete = await _productionOrderRepository.GetById(request.ProductionOrderId);
            if (productionOrdertoDelete == null) throw new EntityNotFoundException(nameof(ProductionOrder), request.ProductionOrderId.ToString());
            if(productionOrdertoDelete.Status != ProductionOrderStatus.Pending || productionOrdertoDelete.Status != ProductionOrderStatus.Cancelled)
            {
                throw new BusinessRuleException($"The order is {productionOrdertoDelete.Status}");
            }
            _productionOrderRepository.Delete(productionOrdertoDelete);

            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
