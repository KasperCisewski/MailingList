using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingEmail;
using MediatR;
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
            var mailingEmail = await _mailingEmailRepository.GetById(request.MailingEmailId);

            await UnsubscribeMailingEmails(mailingEmail, request.UserId);

            return await Task.FromResult(Unit.Value);
        }

        public async Task UnsubscribeMailingEmails(MailingEmail mailingEmail, Guid userId)
        {
            var mailedGroupEmails = mailingEmail.MailingEmailGroups.Where(meg => meg.MailingGroup.UserId == userId);

            foreach (var mailedGroupEmail in mailedGroupEmails)
                await _mailingEmailGroupRepository.Remove(mailedGroupEmail);
        }
    }
}
