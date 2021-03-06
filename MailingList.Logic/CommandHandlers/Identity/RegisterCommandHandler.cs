using MailingList.Data.Domains;
using MailingList.Logic.Commands.Identity;
using MailingList.Logic.Data;
using MailingList.Logic.Models.Responses;
using MailingList.Logic.Services;
using MailingList.Logic.Validators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.Identity
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthorizationSuccessResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly IdentityValidator _identityValidator;
        private readonly UserManager<User> _userManager;

        public RegisterCommandHandler(IIdentityService identityService, IdentityValidator identityValidator, UserManager<User> userManager)
        {
            _identityService = identityService;
            _identityValidator = identityValidator;
            _userManager = userManager;
        }

        public async Task<AuthorizationSuccessResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new LogicException(LogicErrorCode.UserNameDoesNotHaveValue, "Username is required to register");

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new LogicException(LogicErrorCode.EmailDoesNotHaveValue, "Email is required to register");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new LogicException(LogicErrorCode.PasswordDoesNotHaveValue, "Password is required to register");

            if (_identityService.UserWithEmailExists(request.Email))
                throw new LogicException(LogicErrorCode.UserWithSameEmailExist, "Email is not unique. Choose other email");

            if (_identityService.UserWithUsernameExists(request.Username))
                throw new LogicException(LogicErrorCode.UserWithSameUsernameExist, "Username is not unique. Choose other username");

            _identityValidator.ValidateEmail(request.Email);

            _identityValidator.ValidatePassword(request.Password);

            var newUser = new User()
            {
                Email = request.Email,
                UserName = request.Username
            };

            var createdUser = await _userManager.CreateAsync(newUser, request.Password);

            if (!createdUser.Succeeded)
                throw new LogicException(LogicErrorCode.FailedOnUserCreation, $@"Could not create user with username {request.Username}
                    due to errors: {string.Join(',', createdUser.Errors.Select(x => x.Description))}");

            return _identityService.GenerateAuthorizationResultForUser(newUser, request.Secret);
        }
    }
}
