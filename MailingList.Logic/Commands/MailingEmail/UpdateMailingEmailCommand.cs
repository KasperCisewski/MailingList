using MediatR;
using System;

namespace MailingList.Logic.Commands.MailingEmail
{
    public class UpdateMailingEmailCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid MailingGroupId { get; set; }
        public Guid MailingEmailId { get; set; }
        public string NewEmailName { get; set; }
    }
}
