using Domain.OrderBatchs.BatchWeightResults;
using Domain.ProductionOrders.SnapShot;
using Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Materials
{
    public class Material
    {
        public string MaterialId { get; private set; }
        public string MaterialName { get; private set; }
        public string Unit {  get; private set; }
        public bool IsActive { get; private set; } = true;
        public List<RecipeMaterial> RecipeMaterials { get; private set; } = new List<RecipeMaterial>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Material()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Material(string materialName, string unit)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            MaterialName=materialName;
            Unit=unit;
        }
    }
}
