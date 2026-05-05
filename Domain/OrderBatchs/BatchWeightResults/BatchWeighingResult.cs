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
        public Guid BatchWeighingResultId { get; private set; }
        public Guid OrderBatchId { get; private set; }
        public string MaterialId { get; private set; }
        public float TargetKg { get; private set; }
        public float ActualKg { get; private set; }
        public float DeviationKg { get; private set; }
        public float ToleranceMinKg { get; private set; }
        public float ToleranceMaxKg { get; private set; }
        public bool IsWithinTolerance { get; private set; }
        public DateTime CapturedAt { get; private set; } = DateTime.Now;
        public OrderBatch OrderBatch { get; private set; }
        public Material Material { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BatchWeighingResult()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BatchWeighingResult(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            Guid orderBatchId,
            string materialId,
            float targetKg,
            float actualKg,
            float toleranceMinKg,
            float toleranceMaxKg)
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
