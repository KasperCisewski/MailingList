using System.ComponentModel.DataAnnotations;

namespace MailingList.Api.Models.Requests.Identity
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
