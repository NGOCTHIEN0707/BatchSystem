namespace BatchSystem.Application.Commands.Logins.Delete
{
    public class DeleteLoginCommand : IRequest<bool>
    {
        public string UserName { get; set; }

    }
}
