namespace BatchSystem.DTOs
{
    public class ProcessDataRealtimeDto
    {
        public float Raw1SP { get; set; }
        public float Raw1PV { get; set; }
        public float Raw2SP { get; set; }
        public float Raw2PV { get; set; }
        public float Raw3SP { get; set; }
        public float Raw3PV { get; set; }
        public float WaterSP { get; set; }
        public float WaterPV { get; set; }
        public float AdditiveSP { get; set; }
        public float AdditivePV { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
