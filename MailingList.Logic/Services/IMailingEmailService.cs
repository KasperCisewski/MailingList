using System;
using System.Threading.Tasks;

namespace MailingList.Logic.Services
{
    internal interface IMailingEmailService : IService
    {
        Task<Guid> GetOrCreateMailingEmail(string email);
    }
}
