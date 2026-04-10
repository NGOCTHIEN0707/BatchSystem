
using BatchSystem.Domain.Logins;
using Domain.Logins;

namespace BatchSystem.Application.Commands.Logins.Delete
{
    public class DeleteLoginCommandHandler : IRequestHandler<DeleteLoginCommand, bool>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLoginCommandHandler(ILoginRepository loginRepository, IUnitOfWork unitOfWork)
        {
            _loginRepository=loginRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(DeleteLoginCommand request, CancellationToken cancellationToken)
        {
            var loginToDelete = await _loginRepository.GetByName(request.UserName);
            if (loginToDelete == null) throw new EntityNotFoundException(nameof(Login),request.UserName);
            _loginRepository.Delete(loginToDelete);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
