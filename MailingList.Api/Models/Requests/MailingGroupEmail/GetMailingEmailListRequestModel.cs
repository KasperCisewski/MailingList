using MailingList.Api.Models.Requests.Base;
using System;

namespace MailingList.Api.Models.Requests.MailingGroupEmail
{
    public class GetMailingEmailListRequestModel : BasePagingListRequest
    {
        public Guid MailingGroupId { get; set; }
    }
}
