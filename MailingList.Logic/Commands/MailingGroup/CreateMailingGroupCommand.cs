using MediatR;
using System;

namespace MailingList.Logic.Commands.MailingGroup
{
    public class CreateMailingGroupCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
