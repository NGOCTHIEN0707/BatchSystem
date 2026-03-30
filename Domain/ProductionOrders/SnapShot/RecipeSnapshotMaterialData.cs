using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductionOrders.SnapShot
{
    public class RecipeSnapshotMaterialData
    {
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal TargetKg { get; set; }
        public decimal ToleranceMinKg { get; set; }
        public decimal ToleranceMaxKg { get; set; }
    }
}
