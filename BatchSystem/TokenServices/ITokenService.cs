using Domain.Logins;

namespace BatchSystem.TokenServices
{
    public interface ITokenService
    {
        string GenerateToken(Login user);
    }
}
