using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.Identity;
using MailingList.Logic.Data;
using MailingList.Logic.Models.Responses;
using MailingList.Logic.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.Identity
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, AuthorizationSuccessResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public LoginCommandHandler(IIdentityService identityService, IUserRepository userRepository, UserManager<User> userManager)
        {
            _identityService = identityService;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<AuthorizationSuccessResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Login))
                throw new LogicException(LogicErrorCode.LoginDoesNotHaveValue, "Login is required to register");

            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == request.Login || u.UserName == request.Login);

            if (user == null)
                throw new LogicException(LogicErrorCode.CannotFindUser, $"Could not found user with login {request.Login}");

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!userHasValidPassword)
                throw new LogicException(LogicErrorCode.NotValidatedCredentials, $"Could not validate login {request.Password} with given password");

            return _identityService.GenerateAuthorizationResultForUser(user, request.Secret);
        }
    }
}
