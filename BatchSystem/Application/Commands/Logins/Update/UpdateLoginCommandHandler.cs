
using BatchSystem.Domain.Logins;
using Domain.Logins;
using System.Text.RegularExpressions;

namespace BatchSystem.Application.Commands.Logins.Update
{
    public class UpdateLoginCommandHandler : IRequestHandler<UpdateLoginCommand, bool>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLoginCommandHandler(ILoginRepository loginRepository, IUnitOfWork unitOfWork)
        {
            _loginRepository=loginRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(UpdateLoginCommand request, CancellationToken cancellationToken)
        {
            var loginToUpdate = await _loginRepository.GetByName(request.UserName);
            if (loginToUpdate == null)
                throw new EntityNotFoundException(nameof(Login), request.UserName); 

            if (!string.IsNullOrWhiteSpace(request.OldPassWord) &&
                !string.IsNullOrWhiteSpace(request.NewPassWord))
            {
                if (request.NewPassWord == request.OldPassWord)
                    throw new Exception("Mật khẩu mới không được trùng mật khẩu cũ");

                bool isCorrect = SecurePasswordHasher.Verify(request.OldPassWord, loginToUpdate.Password);
                if (!isCorrect)
                    throw new Exception("Mật khẩu cũ không đúng");

                var newHashedPassword = SecurePasswordHasher.Hash(request.NewPassWord);
                loginToUpdate.UpdatePassword(newHashedPassword);
            }

            if (request.Role.HasValue) loginToUpdate.UpdateRole(request.Role.Value);
            if(request.FullName!=null) loginToUpdate.UpdateFullName(request.FullName);
            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                if (!Regex.IsMatch(request.PhoneNumber, @"^(0|\+84)[0-9]{9,10}$"))
                loginToUpdate.UpdatePhoneNumber(request.PhoneNumber);
            }
            
            
            _loginRepository.UpdateAsync(loginToUpdate);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
