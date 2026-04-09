using BatchSystem.Domain.Lines;
using BatchSystem.Domain.SeedWork;
using MediatR;
using BatchSystem.Application.Exceptions;
using Domain.Lines;

namespace BatchSystem.Application.Commands.Lines.Create
{
    public class CreateLineCommandHandler : IRequestHandler<CreateLineCommand, bool>
    {
        private readonly ILineRepository _lineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLineCommandHandler(ILineRepository lineRepository, IUnitOfWork unitOfWork)
        {
            _lineRepository=lineRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(CreateLineCommand request, CancellationToken cancellationToken)
        {
            if (request.LineCode == null || request.LineName ==null)
                throw new Exception("Invalided Data to create Line");

            var lineNameCheck = await _lineRepository.IsLineNameExisted(request.LineName);
            if (lineNameCheck) throw new EntityDuplicationException(nameof(Line), request.LineName);
            var lineCodeCheck = await _lineRepository.IsLineCodeExisted(request.LineCode);
            if (lineCodeCheck) throw new EntityDuplicationException(nameof(Line), request.LineName);

            var lineToCreate = new Line(request.LineName, request.LineCode);
            await _lineRepository.AddAsync(lineToCreate);

            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
