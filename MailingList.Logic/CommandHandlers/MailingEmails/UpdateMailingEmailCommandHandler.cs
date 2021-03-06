using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingEmail;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingEmails
{
    internal class UpdateMailingEmailCommandHandler : IRequestHandler<UpdateMailingEmailCommand>
    {
        private readonly IMailingEmailRepository _mailingEmailRepository;
        private readonly IMailingEmailGroupRepository _mailingEmailGroupRepository;

        public UpdateMailingEmailCommandHandler(IMailingEmailRepository mailingEmailRepository, IMailingEmailGroupRepository mailingEmailGroupRepository)
        {
            _mailingEmailRepository = mailingEmailRepository;
            _mailingEmailGroupRepository = mailingEmailGroupRepository;
        }

        public async Task<Unit> Handle(UpdateMailingEmailCommand request, CancellationToken cancellationToken)
        {
            var mailingEmail = await _mailingEmailRepository.GetById(request.MailingEmailId);

            mailingEmail.

            throw new System.NotImplementedException();
        }
    }
}
