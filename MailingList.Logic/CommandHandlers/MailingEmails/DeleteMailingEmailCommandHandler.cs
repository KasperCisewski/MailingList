using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingEmail;
using MediatR;
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

        public Task<Unit> Handle(DeleteMailingEmailCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
