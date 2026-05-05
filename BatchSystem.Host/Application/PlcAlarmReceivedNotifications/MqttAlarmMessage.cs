namespace BatchSystem.Host.Application.PlcAlarmReceivedNotifications
{
    public class MqttAlarmMessage
    {
        public Guid? OrderBatchId { get; set; } // Gửi từ Gateway để biết thuộc mẻ nào
        public Guid? ProductionOrderId { get; set; }
        public int BatchNo { get; set; }
        public string AlarmId { get; set; } = string.Empty; // Map với AlarmDefinitionId
        public string EventType { get; set; } = string.Empty; // "Raised" hoặc "Cleared"
        public string? AlarmName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
