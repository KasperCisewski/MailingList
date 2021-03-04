namespace MailingList.Api.Models.Requests.Identity
{
    public class UserLoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
