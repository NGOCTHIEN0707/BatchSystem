
using BatchSystem.Domain.Logins;
using Domain.Logins;

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
            if (isUserNameExisted) throw new EntityDuplicationException(nameof(Login),request.UserName);

            var hashedPassword = SecurePasswordHasher.Hash(request.Password);
            var loginToAdd = new Login(request.UserName, hashedPassword, request.FullName,request.PhoneNumber,request.Role);
            await _loginRepository.AddAsync(loginToAdd);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
