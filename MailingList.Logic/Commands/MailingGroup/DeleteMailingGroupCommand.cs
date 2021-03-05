using MediatR;
using System;

namespace MailingList.Logic.Commands.MailingGroup
{
    public class DeleteMailingGroupCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid MailingGroupId { get; set; }
    }
}
