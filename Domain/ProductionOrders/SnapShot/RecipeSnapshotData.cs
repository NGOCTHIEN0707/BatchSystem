using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductionOrders.SnapShot
{
    public class RecipeSnapshotData
    {
        public string RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime SnapshotCreatedAt { get; set; }
        public List<RecipeSnapshotMaterialData> Materials { get; set; } = new List<RecipeSnapshotMaterialData>();



    }
}
