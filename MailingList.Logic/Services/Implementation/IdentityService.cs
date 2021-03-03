using MailingList.Logic.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailingList.Logic.Services.Implementation
{
    public class IdentityService : IIdentityService
    {
        public Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> RegisterAsync(string email, string password, string username)
        {
            throw new NotImplementedException();
        }

        public bool UserWithEmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public bool UserWithUsernameExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}
