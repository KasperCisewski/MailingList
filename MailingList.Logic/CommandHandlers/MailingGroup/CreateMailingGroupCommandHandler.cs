using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingGroup;
using MailingList.Logic.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingGroup
{
    internal class CreateMailingGroupCommandHandler : IRequestHandler<CreateMailingGroupCommand, Guid>
    {
        private readonly IMailingGroupRepository _mailingGroupRepository;
        private readonly IMailingGroupService _mailingGroupService;

        public CreateMailingGroupCommandHandler(IMailingGroupRepository mailingGroupRepository, IMailingGroupService mailingGroupService)
        {
            _mailingGroupRepository = mailingGroupRepository;
            _mailingGroupService = mailingGroupService;
        }

        public async Task<Guid> Handle(CreateMailingGroupCommand request, CancellationToken cancellationToken)
        {
            _mailingGroupService.CheckMailingGroupIsUnique(request.Name);

            var addedMailingGroup = await _mailingGroupRepository.Add(new MailingList.Data.Domains.MailingGroup()
            {
                Name = request.Name,
                UserId = request.UserId
            });

            return addedMailingGroup.Id;
        }
    }
}
