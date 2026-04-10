
using BatchSystem.Domain.Products;
using Domain.Products;

namespace BatchSystem.Application.Commands.Products.DeactivateProduct
{
    public class DeactivateProductCommandHandler : IRequestHandler<DeactivateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository=productRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
        {
            var productToDeactivate = await _productRepository.GetById(request.ProductId);
            if (productToDeactivate == null) throw new EntityNotFoundException(nameof(Product), request.ProductId);
            if (productToDeactivate.IsActive)
            {
                productToDeactivate.DeactivateProduct();
            }
            _productRepository.UpdateAsync(productToDeactivate);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
