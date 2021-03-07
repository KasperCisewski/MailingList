using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Models.Responses;
using MailingList.Logic.Queries.MailingEmail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<MailingEmailModel>> Handle(GetMailingEmailsQuery request, CancellationToken cancellationToken)
        {
            var mailingGroup = await _mailingGroupRepository.GetAll()
                .Include(me => me.MailingEmailGroups)
                .ThenInclude(meg => meg.MailingEmail)
                .FirstOrDefaultAsync(me => me.Id == request.MailingGroupId);

            return mailingGroup.MailingEmailGroups
                .Where(meg => meg.MailingGroup.UserId == request.UserId)
                .Take(request.Take)
                .Skip(request.Skip)
                .Select(meg => new MailingEmailModel()
                {
                    Email = meg.MailingEmail.Email,
                    Id = meg.Id
                });
        }
    }
}
