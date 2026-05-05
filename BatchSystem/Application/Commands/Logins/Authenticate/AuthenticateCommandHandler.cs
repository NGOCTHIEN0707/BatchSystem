using BatchSystem.Domain.Logins;
using BatchSystem.TokenServices;
using Domain.Logins;

namespace BatchSystem.Application.Commands.Logins.Authenticate
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ITokenService _tokenService;

        public AuthenticateCommandHandler(ILoginRepository loginRepository, ITokenService tokenService)
        {
            _loginRepository=loginRepository;
            _tokenService=tokenService;
        }

        public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _loginRepository.GetByName(request.UserName);
            if (user == null) throw new Exception("INVALID_CREDENTIALS, Username or password is incorrect.");
            if (!user.IsActive)
            {
                return new AuthenticateResponse(false, "Account is inactive.");
            }
            var IsSuccess = SecurePasswordHasher.Verify(request.Password, user.Password);
            if (!IsSuccess) return new AuthenticateResponse(false, "Username or password is incorrect.");
            var loginProfile = new InformationAccount(user.UserName, user.FullName, user.Role, user.PhoneNumber);
           // var accessToken = _tokenService.GenerateToken(user);
            var authenticateResult = new AuthenticateResponse(loginProfile);
            return authenticateResult;
        }
    }
}
