using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using System;

namespace MailingList.Data.Repository.Implementation
{
    public class MailingEmailGroupRepository : CrudRepository<MailingEmailGroup, Guid>, IMailingEmailGroupRepository
    {
        public MailingEmailGroupRepository(MailingListDbContext context) : base(context)
        {
        }
    }
}
