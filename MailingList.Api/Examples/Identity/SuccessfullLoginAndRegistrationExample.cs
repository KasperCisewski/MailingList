using MailingList.Logic.Models.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace MailingList.Api.Examples.Identity
{
    public class SuccessfullLoginAndRegistrationExample : IExamplesProvider<AuthorizationSuccessResponse>
    {
        public AuthorizationSuccessResponse GetExamples()
        {
            return new AuthorizationSuccessResponse()
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI61IiwiZW1haWwiOiJ0ZXN0QHRlc3QuY29tIiwiaWQiOiJlYWE4MmUyMC1iN2I1LTQ71FtwBA"
            };
        }
    }
}
