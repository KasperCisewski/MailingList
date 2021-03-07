namespace MailingList.Api.Models
{
    public class ApiErrorResultModel
    {
        public int LogicErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string LogicErrorCodeDescription { get; set; }
    }
}
