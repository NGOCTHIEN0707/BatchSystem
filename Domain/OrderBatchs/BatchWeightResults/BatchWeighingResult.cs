using Domain.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderBatchs.BatchWeightResults
{
    public class BatchWeighingResult
    {
        public string BatchWeighingResultId { get; private set; }
        public string OrderBatchId { get; private set; }
        public string MaterialId { get; private set; }
        public decimal TargetKg { get; private set; }
        public decimal ActualKg { get; private set; }
        public decimal DeviationKg { get; private set; }
        public decimal ToleranceMinKg { get; private set; }
        public decimal ToleranceMaxKg { get; private set; }
        public bool IsWithinTolerance { get; private set; }
        public DateTime CapturedAt { get; private set; } = DateTime.Now;
        public OrderBatch OrderBatch { get; private set; }
        public Material Material { get; private set; }
        public BatchWeighingResult(
            string orderBatchId,
            string materialId,
            decimal targetKg,
            decimal actualKg,
            decimal toleranceMinKg,
            decimal toleranceMaxKg)
        {
            OrderBatchId = orderBatchId;
            MaterialId = materialId;
            TargetKg = targetKg;
            ActualKg = actualKg;
            DeviationKg = actualKg - targetKg;
            ToleranceMinKg = toleranceMinKg;
            ToleranceMaxKg = toleranceMaxKg;
            IsWithinTolerance = actualKg >= toleranceMinKg && actualKg <= toleranceMaxKg;
        }
    }
}
