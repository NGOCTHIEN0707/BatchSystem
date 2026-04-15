using Domain.Logins;

namespace BatchSystem.Application.Commands.Logins.Update
{
    public class UpdateLoginCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string? OldPassWord { get; set; }
        public string? NewPassWord { get; set; }
        public ERole? Role { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
