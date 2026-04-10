namespace BatchSystem.Application.Commands.Materials.Update
{
    public class UpdateMaterialCommand : IRequest<bool>
    {
        public string MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string? Unit {  get; set; }
    }
}
