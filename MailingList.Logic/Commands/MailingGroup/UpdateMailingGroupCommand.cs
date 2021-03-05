using MediatR;
using System;

namespace MailingList.Logic.Commands.MailingGroup
{
    public class UpdateMailingGroupCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid MailingGroupId { get; set; }
        public string NewName { get; set; }
    }
}
