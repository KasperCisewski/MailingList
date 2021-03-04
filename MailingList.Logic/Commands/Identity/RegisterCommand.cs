using MailingList.Logic.Models.Responses;
using MediatR;

namespace MailingList.Logic.Commands.Identity
{
    public class RegisterCommand : IRequest<AuthorizationSuccessResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Secret { get; set; }
    }
}
