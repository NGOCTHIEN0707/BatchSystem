using Domain.Logins;

namespace BatchSystem.Application.Commands.Logins.Create
{
    public class CreateLoginCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ERole Role { get; set; }

    }
}
