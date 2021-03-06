using MediatR;
using System;

namespace MailingList.Logic.Commands.MailingEmail
{
    public class CreateMailingEmailCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid MailingGroupId { get; set; }
        public string Email { get; set; }
    }
}
