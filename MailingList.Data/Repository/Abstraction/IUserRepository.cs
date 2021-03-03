using MailingList.Data.Domains;
using System;

namespace MailingList.Data.Repository.Abstraction
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
