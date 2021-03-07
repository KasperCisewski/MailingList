using MailingList.Data.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MailingList.Logic.Services.Implementation
{
    internal class MailingGroupEmailService : IMailingGroupEmailService
    {
        private readonly IMailingEmailGroupRepository _mailingEmailGroupRepository;

        public MailingGroupEmailService(IMailingEmailGroupRepository mailingEmailGroupRepository)
        {
            _mailingEmailGroupRepository = mailingEmailGroupRepository;
        }

        public async Task DeleteMailingEmailRelatedToMailingGroup(Guid mailingGroupId)
        {
            var mailingGroupEmailsRelatedToMailingGroup = await _mailingEmailGroupRepository.GetAll()
                .Where(meg => meg.MailingGroupId == mailingGroupId)
                .ToListAsync();

            foreach (var mailingGroupEmail in mailingGroupEmailsRelatedToMailingGroup)
                await _mailingEmailGroupRepository.Remove(mailingGroupEmail);
        }
    }
}
