using System;
using System.Threading.Tasks;

namespace MailingList.Logic.Services
{
    internal interface IMailingGroupEmailService : IService
    {
        public Task DeleteMailingEmailRelatedToMailingGroup(Guid mailingGroupId);
    }
}
