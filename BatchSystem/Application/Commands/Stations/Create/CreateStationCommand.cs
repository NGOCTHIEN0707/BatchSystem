namespace BatchSystem.Application.Commands.Stations.Create
{
    public class CreateStationCommand : IRequest<bool>
    {
        public string LineCode { get; set; }
        public string StationName { get; set; }
        public string StationCode { get; set; }
        public int SequenceNo { get; private set; }

    }
}
