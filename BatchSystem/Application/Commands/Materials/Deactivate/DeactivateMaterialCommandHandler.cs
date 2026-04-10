
using BatchSystem.Domain.Materials;
using Domain.Materials;

namespace BatchSystem.Application.Commands.Materials.Deactivate
{
    public class DeactivateMaterialCommandHandler : IRequestHandler<DeactivateMaterialCommand, bool>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateMaterialCommandHandler(IMaterialRepository materialRepository, IUnitOfWork unitOfWork)
        {
            _materialRepository=materialRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(DeactivateMaterialCommand request, CancellationToken cancellationToken)
        {
            var materialToDeactivate = await _materialRepository.GetById(request.MaterialId);
            if (materialToDeactivate == null) throw new EntityNotFoundException(nameof(Material), request.MaterialId);
            if (materialToDeactivate.IsActive)
            {
                materialToDeactivate.Deactivate();
            }
            _materialRepository.UpdateAsync(materialToDeactivate);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
