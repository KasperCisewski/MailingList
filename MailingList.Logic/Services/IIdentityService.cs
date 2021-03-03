using MailingList.Logic.Models.Responses;
using System.Threading.Tasks;

namespace MailingList.Logic.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password, string username);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        public bool UserWithEmailExists(string email);
        public bool UserWithUsernameExists(string username);
    }
}
