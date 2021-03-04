using MailingList.Logic.Models.Responses;
using MediatR;

namespace MailingList.Logic.Commands.Identity
{
    public class LoginCommand : IRequest<AuthorizationSuccessResponse>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Secret { get; set; }
    }
}
