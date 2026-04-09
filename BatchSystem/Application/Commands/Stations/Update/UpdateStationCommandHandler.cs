
using BatchSystem.Domain.Lines;
using BatchSystem.Domain.Stations;
using Domain.Stations;

namespace BatchSystem.Application.Commands.Stations.Update
{
    public class UpdateStationCommandHandler : IRequestHandler<UpdateStationCommand, bool>
    {
        private readonly IStationRepository _stationRepository;
        private readonly ILineRepository _lineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStationCommandHandler(IStationRepository stationRepository, ILineRepository lineRepository, IUnitOfWork unitOfWork)
        {
            _stationRepository=stationRepository;
            _lineRepository=lineRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(UpdateStationCommand request, CancellationToken cancellationToken)
        {
            var stationToUpdate = await _stationRepository.GetById(request.StationId);
            if (stationToUpdate == null) throw new EntityNotFoundException(nameof(Station), request.StationId);
            if (request.LineId!= null)
            {
                var lineCheck = await _lineRepository.GetById(request.LineId);
                stationToUpdate.UpdateLineIdForStation(request.LineId);
            }
            if (request.StationCode!= null) stationToUpdate.UpdateStationCode(request.StationCode);
            if (request.StationName!= null) stationToUpdate.UpdateStationName(request.StationName);
            if (request.SequenceNo != null) stationToUpdate.UpdateSequenceNo(request.SequenceNo.Value);

            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);


        }
    }
}
