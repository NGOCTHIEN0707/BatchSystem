
using BatchSystem.Domain.Lines;
using BatchSystem.Domain.Stations;
using Domain.Lines;
using Domain.Stations;

namespace BatchSystem.Application.Commands.Stations.Create
{
    public class CreateStationCommandHandler : IRequestHandler<CreateStationCommand, bool>
    {
        private readonly IStationRepository _stationRepository;
        private readonly ILineRepository _lineRepository;
        private readonly IUnitOfWork _unitOfWork;
        

        public CreateStationCommandHandler(IStationRepository stationRepository, ILineRepository lineRepository, IUnitOfWork unitOfWork)
        {
            _stationRepository=stationRepository;
            _lineRepository=lineRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(CreateStationCommand request, CancellationToken cancellationToken)
        {
            var line = await _lineRepository.GetByLineCode(request.LineCode);
            if(line == null) throw new EntityNotFoundException(nameof(Line),request.LineCode);

            
            if (request.StationCode == null || request.StationName ==null)
                throw new Exception("Invalided Data to create Station");

            var stationNameCheck = await _stationRepository.IsStationNameExisted(request.StationName);
            if (stationNameCheck) throw new EntityDuplicationException(nameof(Station), request.StationName);
            var stationCodeCheck = await _stationRepository.IsStationCodeExisted(request.StationCode);
            if (stationCodeCheck) throw new EntityDuplicationException(nameof(Station), request.StationCode);
            var stationToCreaet = new Station(request.StationCode, request.StationName,line.LineId, request.SequenceNo);
            
            await _stationRepository.AddAsync(stationToCreaet);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);


        }
    }
}
