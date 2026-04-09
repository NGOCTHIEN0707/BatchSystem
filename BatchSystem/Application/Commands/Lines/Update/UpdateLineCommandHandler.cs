using BatchSystem.Domain.Lines;
using BatchSystem.Domain.SeedWork;
using Domain.Lines;
using MediatR;

namespace BatchSystem.Application.Commands.Lines.Update
{
    public class UpdateLineCommandHandler : IRequestHandler<UpdateLineCommand, bool>
    {
        private readonly ILineRepository _lineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLineCommandHandler(ILineRepository lineRepository, IUnitOfWork unitOfWork)
        {
            _lineRepository=lineRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(UpdateLineCommand request, CancellationToken cancellationToken)
        {
            var lineToUpdate = await _lineRepository.GetById(request.LineId);
            if (lineToUpdate == null) throw new EntityNotFoundException(nameof(Line), request.LineId);
            if (request.LineName != null) lineToUpdate.UpdateLineName(request.LineName);
            if (request.LineCode != null) lineToUpdate.UpdateLineCode(request.LineCode);
            _lineRepository.UpdateAsync(lineToUpdate);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
