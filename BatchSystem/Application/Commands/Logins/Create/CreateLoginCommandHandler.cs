
using BatchSystem.Domain.Logins;
using BatchSystem.Domain.Logins.StaffCodes;
using Domain.Logins;
using System.Text.RegularExpressions;

namespace BatchSystem.Application.Commands.Logins.Create
{
    public class CreateLoginCommandHandler : IRequestHandler<CreateLoginCommand, bool>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffCodeRepository _staffCodeRepository;

        public CreateLoginCommandHandler(ILoginRepository loginRepository, IUnitOfWork unitOfWork, IStaffCodeRepository staffCodeRepository)
        {
            _loginRepository=loginRepository;
            _unitOfWork=unitOfWork;
            _staffCodeRepository=staffCodeRepository;
        }

        public async Task<bool> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var isUserNameExisted = await _loginRepository.IsUserNameExisted(request.UserName);
            if (isUserNameExisted) throw new EntityDuplicationException(nameof(Login), request.UserName);
            if (string.IsNullOrWhiteSpace(request.PhoneNumber) || !Regex.IsMatch(request.PhoneNumber, @"^(0|\+84)[0-9]{9,10}$"))
            {
                throw new ArgumentException("Phone number is invalid");
            }

            ERole role;

            // 🔥 CASE 1: Staff / Manager → bắt buộc có code
            if (request.Role == ERole.Staff || request.Role == ERole.Manager)
            {
                if (!request.StaffCode.HasValue)
                    throw new ArgumentException("Staff code is required");

                var staffCode = await _staffCodeRepository.GetStaffCodeByCode(request.StaffCode.Value);

                if (staffCode == null)
                    throw new ArgumentException("Staff code is invalid");

                if (staffCode.IsUsed)
                    throw new ArgumentException("Staff code has already been used");
                
                if (staffCode.Role != request.Role)
                    throw new ArgumentException("Staff code does not match selected role");
                
                role = staffCode.Role;

                staffCode.MarkAsUsed();
            }
            else
            {
                // 🔥 CASE 2: Customer → không cần code
                role = request.Role;
            }


            var hashedPassword = SecurePasswordHasher.Hash(request.Password);

            var loginToAdd = new Login(request.UserName, hashedPassword, request.FullName, request.PhoneNumber, role);
            await _loginRepository.AddAsync(loginToAdd);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
