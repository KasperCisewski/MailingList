using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MailingList.Tests.Integration.Infrastructure.Validators
{
    public class ApiRequestValidator
    {
        public void EnsureRequestSuccess<T>(IActionResult actionResult)
        {
            Assert.IsType<T>(actionResult);
        }
    }
}
