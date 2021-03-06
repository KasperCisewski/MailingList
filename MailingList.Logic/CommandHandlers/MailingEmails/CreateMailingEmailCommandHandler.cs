using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Commands.MailingEmail;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.CommandHandlers.MailingEmails
{
    internal class CreateMailingEmailCommandHandler : IRequestHandler<CreateMailingEmailCommand, Guid>
    {
        private readonly IMailingEmailRepository _mailingEmailRepository;

        public CreateMailingEmailCommandHandler(IMailingEmailRepository mailingEmailRepository)
        {
            _mailingEmailRepository = mailingEmailRepository;
        }

        public Task<Guid> Handle(CreateMailingEmailCommand request, CancellationToken cancellationToken)
        {
            
            throw new NotImplementedException();
        }
    }
}
