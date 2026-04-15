using Domain.ProductionOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logins
{
    public class Login
    {
        public string LoginId { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string FullName { get; private set; }
        public string PhoneNumber { get; set; }
        public ERole Role { get; private set; }
        public bool IsActive { get; private set; } = true;
        public List<ProductionOrder> ProductionOrders { get; private set; } = new List<ProductionOrder>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Login()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
        public void Deactivate()
        {
            IsActive = false;
        }
        public Login(string userName, string password, string fullName, string phoneNumber, ERole role)
        {
            UserName=userName;
            Password=password;
            FullName=fullName;
            PhoneNumber=phoneNumber;
            Role=role;
        }
        public void UpdateFullName (string fullName) => FullName = fullName;
        public void UpdatePhoneNumber(string phoneNumber ) => PhoneNumber = phoneNumber;
        public void UpdateUserName(string userName)
        {
            UserName = userName;
        }
        public void UpdatePassword(string newPassword)
        {
            Password = newPassword;
        }
        public void UpdateRole(ERole role)
        {
            Role = role;
        }
    }
}
