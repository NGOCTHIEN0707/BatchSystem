using Domain.Logins;

namespace BatchSystem.Application.Commands.Logins.Authenticate
{
    public class InformationAccount
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public ERole Role { get; set; }
        public int PhoneNumber { get; set; }

        public InformationAccount(string userName, string fullName, ERole role, int phoneNumber)
        {
            UserName=userName;
            FullName=fullName;
            Role=role;
            PhoneNumber=phoneNumber;
        }
    }
}
