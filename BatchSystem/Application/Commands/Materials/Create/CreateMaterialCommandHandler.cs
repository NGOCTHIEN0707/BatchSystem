
using BatchSystem.Domain.Materials;
using Domain.Materials;

namespace BatchSystem.Application.Commands.Materials.Create
{
    public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, bool>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMaterialCommandHandler(IMaterialRepository materialRepository, IUnitOfWork unitOfWork)
        {
            _materialRepository=materialRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            var isMaterialNameExisted = await _materialRepository.IsMaterialNameExisted(request.MaterialName);
            if (isMaterialNameExisted) throw new EntityDuplicationException(nameof(Material),request.MaterialName);
            if (request.MaterialName == null || request.Unit == null) throw new Exception("Invalid Data to create Material");
            var materialToCreate = new Material(request.MaterialName, request.Unit);
            
            await _materialRepository.AddAsync(materialToCreate);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
            
        }
    }
}
