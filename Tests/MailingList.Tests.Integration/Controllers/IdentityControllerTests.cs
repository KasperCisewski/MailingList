using MailingList.Api.Controllers;
using MailingList.Api.Models.Requests.Identity;
using MailingList.Logic.Models.Responses;
using MailingList.Tests.Integration.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace MailingList.Tests.Integration.Controllers
{
    [Collection("Db")]
    public class IdentityControllerTests : BaseController
    {
        [Fact]
        public async Task LoginIntoApp_AccountCreatedInDatabase_ShouldReturnsGuid()
        {
            var identityController = Host.Services.GetService<IdentityController>();

            var actionResult = await identityController.Login(new UserLoginRequest()
            {
                Login = "test123",
                Password = "Test123!"
            });

            ApiRequestValidator.EnsureRequestSuccess<OkObjectResult>(actionResult);

            Assert.IsType<AuthorizationSuccessResponse>(((OkObjectResult)actionResult).Value);
        }
    }
}
