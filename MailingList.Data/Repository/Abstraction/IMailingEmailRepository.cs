using MailingList.Data.Domains;
using System;

namespace MailingList.Data.Repository.Abstraction
{
    public interface IMailingEmailRepository : IRepository<MailingEmail, Guid>
    {
    }
}
