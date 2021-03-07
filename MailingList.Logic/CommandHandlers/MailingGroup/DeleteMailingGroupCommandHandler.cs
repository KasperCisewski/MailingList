using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingGroup;
using MailingList.Logic.Data;
using MailingList.Logic.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingGroup
{
    internal class DeleteMailingGroupCommandHandler : IRequestHandler<DeleteMailingGroupCommand>
    {
        private readonly IMailingGroupRepository _mailingGroupRepository;
        private readonly IMailingGroupEmailService _mailingGroupEmailService;

        public DeleteMailingGroupCommandHandler(IMailingGroupRepository mailingGroupRepository, IMailingGroupEmailService mailingGroupEmailService)
        {
            _mailingGroupRepository = mailingGroupRepository;
            _mailingGroupEmailService = mailingGroupEmailService;
        }

        public async Task<Unit> Handle(DeleteMailingGroupCommand request, CancellationToken cancellationToken)
        {
            var mailingGroup = await _mailingGroupRepository.GetById(request.MailingGroupId);

            if (mailingGroup.UserId != request.UserId)
                throw new LogicException(LogicErrorCode.DisallowToMakeChangesInOtherUserMailingGroup, "Could not delete mailing group which is not belong to user");

            await _mailingGroupEmailService.DeleteMailingEmailRelatedToMailingGroup(mailingGroup.Id);

            await _mailingGroupRepository.Remove(mailingGroup);

            return await Task.FromResult(Unit.Value);
        }
    }
}
