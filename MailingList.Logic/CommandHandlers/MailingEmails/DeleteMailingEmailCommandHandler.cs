using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingEmail;
using MailingList.Logic.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingEmails
{
    internal class DeleteMailingEmailCommandHandler : IRequestHandler<DeleteMailingEmailCommand>
    {
        private readonly IMailingEmailRepository _mailingEmailRepository;
        private readonly IMailingEmailGroupRepository _mailingEmailGroupRepository;

        public DeleteMailingEmailCommandHandler(IMailingEmailRepository mailingEmailRepository, IMailingEmailGroupRepository mailingEmailGroupRepository)
        {
            _mailingEmailRepository = mailingEmailRepository;
            _mailingEmailGroupRepository = mailingEmailGroupRepository;
        }

        public async Task<Unit> Handle(DeleteMailingEmailCommand request, CancellationToken cancellationToken)
        {
            var mailingEmail = _mailingEmailRepository
                .GetAll()
                .AsNoTracking()
                .Include(me => me.MailingEmailGroups)
                .ThenInclude(me => me.MailingGroup)
                .FirstOrDefault(me => me.Id == request.MailingEmailId);

            if (mailingEmail == null)
                throw new LogicException(LogicErrorCode.CouldNotFindMailingEmail, $"Could not find any mailing email with id {request.MailingEmailId}");

            if (!mailingEmail.MailingEmailGroups.Any(meg => meg.MailingGroup.UserId == request.UserId))
                throw new LogicException(LogicErrorCode.DisallowToMakeChangesInOtherUserMailingGroup, "Could not update mailing group which is not belong to user");

            await UnsubscribeMailingEmails(mailingEmail, request.UserId);

            return await Task.FromResult(Unit.Value);
        }

        public async Task UnsubscribeMailingEmails(MailingEmail mailingEmail, Guid userId)
        {
            var mailedGroupEmails = mailingEmail.MailingEmailGroups.Where(meg => meg.MailingGroup.UserId == userId).ToList();

            foreach (var mailedGroupEmail in mailedGroupEmails)
                await _mailingEmailGroupRepository.Remove(mailedGroupEmail);
        }
    }
}
