using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingGroup;
using MailingList.Logic.Data;
using MailingList.Logic.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingGroup
{
    internal class UpdateMailingGroupCommandHandler : IRequestHandler<UpdateMailingGroupCommand>
    {
        private readonly IMailingGroupRepository _mailingGroupRepository;
        private readonly IMailingGroupService _mailingGroupService;

        public UpdateMailingGroupCommandHandler(IMailingGroupRepository mailingGroupRepository, IMailingGroupService mailingGroupService)
        {
            _mailingGroupRepository = mailingGroupRepository;
            _mailingGroupService = mailingGroupService;
        }

        public async Task<Unit> Handle(UpdateMailingGroupCommand request, CancellationToken cancellationToken)
        {
            var mailingGroup = await _mailingGroupRepository.GetById(request.MailingGroupId);

            if (mailingGroup.UserId != request.UserId)
                throw new LogicException(LogicErrorCode.DisallowToMakeChangesInOtherUserMailingGroup, "Could not update mailing group which is not belong to us");

            _mailingGroupService.CheckMailingGroupIsUnique(request.NewName);

            mailingGroup.Name = request.NewName;

            await _mailingGroupRepository.Update(mailingGroup);

            return await Task.FromResult(Unit.Value);
        }
    }
}
