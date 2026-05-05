namespace BatchSystem.Host.Application.PlcAckReceivedNotifications
{
    public class MqttAckMessage
    {
        public Guid OrderBatchId { get; set; }
        public int BatchNo { get; set; }
        public string Status { get; set; } // Received / Running / Completed / Error
        public List<MqttWeighingResultMessage> WeighingResults { get; set; } = new();
        public DateTime Timestamp { get; set; }

    }
    public class MqttWeighingResultMessage
    {
        public string MaterialId { get; set; } = "";
        public float TargetKg { get; set; }
        public float ActualKg { get; set; }
    }
}
