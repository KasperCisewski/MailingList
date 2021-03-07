using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingEmail;
using MailingList.Logic.Data;
using MailingList.Logic.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingEmails
{
    internal class UpdateMailingEmailCommandHandler : IRequestHandler<UpdateMailingEmailCommand>
    {
        private readonly IMailingEmailRepository _mailingEmailRepository;
        private readonly IMailingEmailGroupRepository _mailingEmailGroupRepository;
        private readonly IMailingEmailService _mailingEmailService;
        private readonly IMailingGroupRepository _mailingGroupRepository;

        public UpdateMailingEmailCommandHandler(IMailingEmailRepository mailingEmailRepository, IMailingEmailGroupRepository mailingEmailGroupRepository,
            IMailingEmailService mailingEmailService, IMailingGroupRepository mailingGroupRepository)
        {
            _mailingEmailRepository = mailingEmailRepository;
            _mailingEmailGroupRepository = mailingEmailGroupRepository;
            _mailingEmailService = mailingEmailService;
            _mailingGroupRepository = mailingGroupRepository;
        }

        public async Task<Unit> Handle(UpdateMailingEmailCommand request, CancellationToken cancellationToken)
        {
            var mailingEmail = await _mailingEmailRepository.GetAll()
                .Include(me => me.MailingEmailGroups)
                .FirstOrDefaultAsync(me => me.Id == request.MailingEmailId);

            if (mailingEmail == null)
                throw new LogicException(LogicErrorCode.CannotFindMailingEmail, $"Could not find mailing email with id {request.MailingEmailId}");

            if (mailingEmail.Email != request.NewEmailName)
                throw new LogicException(LogicErrorCode.NewNameAndOldNameShouldBeDifferent, "Could not update mailing email to exactly same mailing email");

            var mailingGroup = await _mailingGroupRepository.GetAll().FirstOrDefaultAsync(mg => mg.Id == request.MailingGroupId);

            if (mailingGroup == null)
                throw new LogicException(LogicErrorCode.CannotFindMailingGroup, $"Could not find mailing email group with id {request.MailingGroupId}");

            var newMailingEmailId = await _mailingEmailService.GetOrCreateMailingEmail(request.NewEmailName);

            var mailingGroupEmail = mailingEmail.MailingEmailGroups
                .FirstOrDefault(meg => meg.MailingGroup.UserId == request.UserId && meg.MailingGroupId == request.MailingGroupId);

            if (mailingGroupEmail == null)
                throw new LogicException(LogicErrorCode.CannotFindMailingEmail,
                    $"Could not find mailing email group for user with id {request.UserId}, mailing group with id {request.MailingGroupId} and mailing email with id {mailingEmail.Id}");

            await _mailingEmailGroupRepository.Remove(mailingGroupEmail);

            var newMailingGroup = new MailingEmailGroup()
            {
                MailingEmailId = newMailingEmailId,
                MailingGroupId = mailingGroup.Id
            };

            await _mailingEmailGroupRepository.Add(newMailingGroup);

            return await Task.FromResult(Unit.Value);
        }
    }
}
