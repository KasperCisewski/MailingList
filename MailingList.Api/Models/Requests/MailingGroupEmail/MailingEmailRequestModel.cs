using System;

namespace MailingList.Api.Models.Requests.MailingGroupEmail
{
    public class MailingEmailRequestModel
    {
        public Guid MailingGroupId { get; set; }
        public string Email { get; set; }
    }
}
