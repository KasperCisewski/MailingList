using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using System;

namespace MailingList.Data.Repository.Implementation
{
    public class MailingEmailRepository : CrudRepository<MailingEmail, Guid>, IMailingEmailRepository
    {
        public MailingEmailRepository(MailingListDbContext context) : base(context)
        {
        }
    }
}
