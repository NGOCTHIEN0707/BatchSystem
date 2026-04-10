namespace BatchSystem.Application.Commands.Materials.Deactivate
{
    public class DeactivateMaterialCommand : IRequest<bool>
    {
        public string MaterialId { get; set; }
    }
}
