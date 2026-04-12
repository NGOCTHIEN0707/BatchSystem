namespace BatchSystem.Application.Queries.GetAllWithRecipe
{
    public class RecipeMaterialDto
    {
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal TargetKg { get; set; }
        public decimal ToleranceMinKg { get; set; }
        public decimal ToleranceMaxKg { get; set; }
    }
}
