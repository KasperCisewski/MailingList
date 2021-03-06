using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingGroup;
using MailingList.Logic.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingGroup
{
    internal class DeleteMailingGroupCommandHandler : IRequestHandler<DeleteMailingGroupCommand>
    {
        private readonly IMailingGroupRepository _mailingGroupRepository;
        private readonly IMailingEmailGroupRepository _mailingEmailGroupRepository;

        public DeleteMailingGroupCommandHandler(IMailingGroupRepository mailingGroupRepository, IMailingEmailGroupRepository mailingEmailGroupRepository)
        {
            _mailingGroupRepository = mailingGroupRepository;
            _mailingEmailGroupRepository = mailingEmailGroupRepository;
        }

        public async Task<Unit> Handle(DeleteMailingGroupCommand request, CancellationToken cancellationToken)
        {
            var mailingGroup = await _mailingGroupRepository.GetById(request.MailingGroupId);

            if (mailingGroup.UserId != request.UserId)
                throw new LogicException(LogicErrorCode.DisallowToMakeChangesInOtherUserMailingGroup, "Could not delete mailing group which is not belong to us");

            await DeleteMailingEmailRelatedToMailingGroup(mailingGroup.Id);

            await _mailingGroupRepository.Remove(mailingGroup);

            return await Task.FromResult(Unit.Value);
        }

        private async Task DeleteMailingEmailRelatedToMailingGroup(Guid mailingGroupId)
        {
            var mailingGroupEmailsRelatedToMailingGroup = await _mailingEmailGroupRepository.GetAll()
                .Where(meg => meg.MailingGroupId == mailingGroupId)
                .ToListAsync();

            foreach (var mailingGroupEmail in mailingGroupEmailsRelatedToMailingGroup)
                await _mailingEmailGroupRepository.Remove(mailingGroupEmail);
        }
    }
}
