using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using System;

namespace MailingList.Data.Repository.Implementation
{
    public class MailingGroupRepository : CrudRepository<MailingGroup, Guid>, IMailingGroupRepository
    {
        public MailingGroupRepository(MailingListDbContext context) : base(context)
        {
        }
    }
}
