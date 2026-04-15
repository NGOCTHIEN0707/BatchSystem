
using BatchSystem.Domain.Logins;
using Domain.Logins;
using System.Text.RegularExpressions;

namespace BatchSystem.Application.Commands.Logins.Create
{
    public class CreateLoginCommandHandler : IRequestHandler<CreateLoginCommand, bool>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLoginCommandHandler(ILoginRepository loginRepository, IUnitOfWork unitOfWork)
        {
            _loginRepository=loginRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var isUserNameExisted = await _loginRepository.IsUserNameExisted(request.UserName);
            if (isUserNameExisted) throw new EntityDuplicationException(nameof(Login), request.UserName);
            if (string.IsNullOrWhiteSpace(request.PhoneNumber) || !Regex.IsMatch(request.PhoneNumber, @"^(0|\+84)[0-9]{9,10}$"))
            {
                throw new ArgumentException("Phone number is invalid");
            }
            var hashedPassword = SecurePasswordHasher.Hash(request.Password);

            var loginToAdd = new Login(request.UserName, hashedPassword, request.FullName, request.PhoneNumber, request.Role);
            await _loginRepository.AddAsync(loginToAdd);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
