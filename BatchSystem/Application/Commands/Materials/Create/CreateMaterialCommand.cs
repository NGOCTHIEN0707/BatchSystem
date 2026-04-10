namespace BatchSystem.Application.Commands.Materials.Create
{
    public class CreateMaterialCommand : IRequest<bool>
    {
        public string MaterialName { get; set; }
        public string Unit {  get; set; }
    }
}
