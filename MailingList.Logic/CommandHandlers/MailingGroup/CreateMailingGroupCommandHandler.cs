using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingGroup;
using MailingList.Logic.Data;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingGroup
{
    public class CreateMailingGroupCommandHandler : IRequestHandler<CreateMailingGroupCommand, Guid>
    {
        private readonly IMailingGroupRepository _mailingGroupRepository;

        public CreateMailingGroupCommandHandler(IMailingGroupRepository mailingGroupRepository)
        {
            _mailingGroupRepository = mailingGroupRepository;
        }

        public async Task<Guid> Handle(CreateMailingGroupCommand request, CancellationToken cancellationToken)
        {
            if (_mailingGroupRepository.GetAll().Any(mg => mg.Name.ToLower() == request.Name.ToLower()))
                throw new LogicException(LogicErrorCode.MailingGroupNameMustBeUnique , $"Name {request.Name} is not unique name for mailing group.");

            var addedMailingGroup = await _mailingGroupRepository.Add(new MailingList.Data.Domains.MailingGroup()
            {
                Name = request.Name,
                UserId = request.UserId
            });

            return addedMailingGroup.Id;
        }
    }
}
