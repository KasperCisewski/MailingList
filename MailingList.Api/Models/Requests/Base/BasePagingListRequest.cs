namespace MailingList.Api.Models.Requests.Base
{
    public class BasePagingListRequest
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
