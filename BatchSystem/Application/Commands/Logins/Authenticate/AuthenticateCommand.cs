using Microsoft.VisualBasic;

namespace BatchSystem.Application.Commands.Logins.Authenticate
{
    public class AuthenticateCommand : IRequest<AuthenticateResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}
