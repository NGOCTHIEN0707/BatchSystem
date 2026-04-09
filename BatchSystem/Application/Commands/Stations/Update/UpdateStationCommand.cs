namespace BatchSystem.Application.Commands.Stations.Update
{
    public class UpdateStationCommand : IRequest<bool>
    {
        public string StationId { get; set; }
        public string? StationName { get; set; }
        public string? LineId { get; set; }
        public string? StationCode { get; set; }
        public int? SequenceNo { get; set; }
    }
}
