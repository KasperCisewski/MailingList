using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MailingList.Tests.Integration.Validators
{
    public class ApiRequestValidator
    {
        public void EnsureRequestSuccess<T>(IActionResult actionResult)
        {
            Assert.IsType<T>(actionResult);
        }
    }
}
