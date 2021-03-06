using MailingList.Logic.Data;

namespace MailingList.Api.Models
{
    public class ApiErrorResultModel
    {
        public LogicErrorCode LogicErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
