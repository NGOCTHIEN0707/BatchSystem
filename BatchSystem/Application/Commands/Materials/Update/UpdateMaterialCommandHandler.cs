
using BatchSystem.Domain.Materials;
using Domain.Materials;

namespace BatchSystem.Application.Commands.Materials.Update
{
    public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand, bool>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMaterialCommandHandler(IMaterialRepository materialRepository, IUnitOfWork unitOfWork)
        {
            _materialRepository=materialRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            var materialToUpdate = await _materialRepository.GetById(request.MaterialId);
            if (materialToUpdate == null) throw new EntityNotFoundException(nameof(Material), request.MaterialId);

            if (request.MaterialName!=null)
            {
                var isMaterialNameExisted = await _materialRepository.IsMaterialNameExisted(materialToUpdate.MaterialName);
                if (isMaterialNameExisted) throw new EntityDuplicationException(nameof(Material), request.MaterialName);
                materialToUpdate.UpdateMaterialName(request.MaterialName);
            }
            if(request.Unit != null) materialToUpdate.UpdateUnit(request.Unit);

            _materialRepository.UpdateAsync(materialToUpdate);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
