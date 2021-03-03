using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using System;

namespace MailingList.Data.Repository.Implementation
{
    public class UserRepository : CrudRepository<User, Guid>, IUserRepository
    {
        public UserRepository(MailingListDbContext context) : base(context)
        {
        }
    }
}
