using MediatR;
using System;

namespace MailingList.Logic.Commands.MailingEmail
{
    public class DeleteMailingEmailCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid MailingEmailId { get; set; }
    }
}
