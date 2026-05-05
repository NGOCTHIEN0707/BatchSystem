namespace BatchSystem.Host.Application.PlcStatusReceivedNotifications
{
    public class MqttStatusMessage
    {
        //public Guid? OrderBatchId { get; set; }
        public Guid? ProductionOrderDetailId { get; set; }
        public Guid? ActiveOrderBatchId { get; set; }
        public Guid? ProcessingOrderBatchId { get; set; }

        public int ActiveBatchNo { get; set; }
        public ushort ActiveBatchStepNo { get; set; }
        public ushort ActiveBatchPhaseState { get; set; }

        public int ProcessingBatchNo { get; set; }
        public ushort ProcessingBatchStepNo { get; set; }
        public ushort ProcessingBatchPhaseState { get; set; }
        public int BatchNo { get; set; }

        // Auto / Manual
        public ushort OperationMode { get; set; }

        // Trạng thái hệ thống / batch
        public ushort ControlState { get; set; }
        public ushort BatchState { get; set; }

        // Công đoạn
        //public ushort StepNo { get; set; }
        //public ushort PhaseState { get; set; }

        // Alarm summary
        public ushort AlarmSummaryWord { get; set; }
        public ushort ActiveAlarmCount { get; set; }

        public uint Heartbeat { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
