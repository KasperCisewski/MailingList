using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingGroup;
using MailingList.Logic.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingGroup
{
    class UpdateMailingGroupCommandHandler : IRequestHandler<UpdateMailingGroupCommand>
    {
        private readonly IMailingGroupRepository _mailingGroupRepository;

        public UpdateMailingGroupCommandHandler(IMailingGroupRepository mailingGroupRepository)
        {
            _mailingGroupRepository = mailingGroupRepository;
        }

        public async Task<Unit> Handle(UpdateMailingGroupCommand request, CancellationToken cancellationToken)
        {
            var mailingGroup = await _mailingGroupRepository.GetById(request.MailingGroupId);

            if (mailingGroup.UserId != request.UserId)
                throw new LogicException(LogicErrorCode.DisallowToMakeChangesInOtherUserMailingGroup, "Could not update mailing group which is not belong to us");

            mailingGroup.Name = request.NewName;

            await _mailingGroupRepository.Update(mailingGroup);

            return await Task.FromResult(Unit.Value);
        }
    }
}
