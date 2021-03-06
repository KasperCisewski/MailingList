using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Data;
using MailingList.Logic.Models.Responses;
using MailingList.Logic.Queries.MailingEmail;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.QueryHandlers.MailingEmails
{
    internal class GetMailingEmailsQueryHandler : IRequestHandler<GetMailingEmailsQuery, IEnumerable<MailingEmailModel>>
    {
        private readonly IMailingEmailRepository _mailingEmailRepository;
        private readonly IMailingGroupRepository _mailingGroupRepository;

        public GetMailingEmailsQueryHandler(IMailingEmailRepository mailingEmailRepository, IMailingGroupRepository mailingGroupRepository)
        {
            _mailingEmailRepository = mailingEmailRepository;
            _mailingGroupRepository = mailingGroupRepository;
        }

        public Task<IEnumerable<MailingEmailModel>> Handle(GetMailingEmailsQuery request, CancellationToken cancellationToken)
        {
            var mailingGroup = _mailingEmailRepository.GetById(request.MailingGroupId);

            if (mailingGroup == null)
                throw new LogicException(LogicErrorCode., "");

            throw new System.NotImplementedException();
        }
    }
}
