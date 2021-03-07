using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingEmail;
using MailingList.Logic.Data;
using MailingList.Logic.Services;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingEmails
{
    internal class CreateMailingEmailCommandHandler : IRequestHandler<CreateMailingEmailCommand, Guid>
    {
        private readonly IMailingEmailRepository _mailingEmailRepository;
        private readonly IMailingEmailService _mailingEmailService;
        private readonly IMailingGroupRepository _mailingGroupRepository;
        private readonly IMailingEmailGroupRepository _mailingEmailGroupRepository;

        public CreateMailingEmailCommandHandler(IMailingEmailRepository mailingEmailRepository, IMailingEmailService mailingEmailService,
            IMailingGroupRepository mailingGroupRepository, IMailingEmailGroupRepository mailingEmailGroupRepository)
        {
            _mailingEmailRepository = mailingEmailRepository;
            _mailingEmailService = mailingEmailService;
            _mailingGroupRepository = mailingGroupRepository;
            _mailingEmailGroupRepository = mailingEmailGroupRepository;
        }

        public async Task<Guid> Handle(CreateMailingEmailCommand request, CancellationToken cancellationToken)
        {
            var mailingEmailId = await _mailingEmailService.GetOrCreateMailingEmail(request.Email);

            var mailingGroup = await _mailingGroupRepository.GetById(request.MailingGroupId);

            if (mailingGroup == null)
                throw new LogicException(LogicErrorCode.CouldNotFindMailingGroup, $"Could not found mailing group with id '{request.MailingGroupId}'");

            if (mailingGroup.UserId != request.UserId)
                throw new LogicException(LogicErrorCode.DisallowToMakeChangesInOtherUserMailingGroup, "Could not update mailing group which is not belong to user");

            var mailingEmailGroup = _mailingEmailGroupRepository.GetAll()
                .FirstOrDefault(meg => meg.MailingGroupId == request.MailingGroupId && meg.MailingEmailId == mailingEmailId);

            if (mailingEmailGroup != null)
                throw new LogicException(LogicErrorCode.MailingEmailGrourExist, "Actually there is exist mailing email which is subscribed to mailing group");

            var newMailingGroup = new MailingEmailGroup()
            {
                MailingEmailId = mailingEmailId,
                MailingGroupId = mailingGroup.Id
            };

            await _mailingEmailGroupRepository.Add(newMailingGroup);

            return newMailingGroup.Id;
        }
    }
}
