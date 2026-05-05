namespace BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers
{
    public class LoadBatchCommandMessage
    {
        public string Command { get; set; } = "LoadBatch";
        public string ProductionOrderId { get; set; } = default!;
        public string ProductionOrderDetailId { get; set; } = default!;
        public int StartBatchNo { get; set; }
        public int BatchCount { get; set; }
        public int ProductCode { get; set; }
        public int CustomerCode { get; set; }
        public int GrindingTimeSeconds { get; set; }
        public int MixingTimeSeconds { get; set; }
        public int NumberOfPieces { get; set; }
        public float WeightOfAPiece { get; set; }
        public List<LoadBatchMaterialMessage> Materials { get; set; } = new();
        public List<LoadBatchItemMessage> Batches { get; set; } = new();
        public DateTime Timestamp { get; set; }
    }
    public class LoadBatchItemMessage
    {
        public string OrderBatchId { get; set; } = default!;
        public int BatchNo { get; set; }
    }
    public class LoadBatchMaterialMessage
    {
        public string MaterialId { get; set; } = default!;
        public int SequenceNo { get; set; }
        public float TargetKg { get; set; }

    }
}
