using MailingList.Api.Controllers;
using MailingList.Api.Models.Requests.MailingGroupEmail;
using MailingList.Data;
using MailingList.Logic.Models.Responses;
using MailingList.Tests.Integration.Controllers.Base;
using MailingList.Tests.Integration.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MailingList.Tests.Integration.Controllers
{
    [Collection("Db")]
    public class MailingEmailControllerTests : BaseController
    {
        private MailingEmailController _mailingEmailController;
        public MailingEmailControllerTests()
        {
            _mailingEmailController = Host.Services.GetService<MailingEmailController>();
        }

        [Fact]
        public async Task CreateMailingEmail_MailingGroupAndMailingEmailNotExists_ShouldReturnsGuid()
        {
            var mailingListDbContext = Host.Services.GetService<MailingListDbContext>();

            var user = mailingListDbContext.Users.First();
            _mailingEmailController.SetFakeHttpContextAndSetUser(user.Id);
            var mailingGroup = mailingListDbContext.MailingGroup.First(m => m.UserId == user.Id);

            var actionResult = await _mailingEmailController.Create(new MailingEmailRequestModel()
            {
                Email = "testEmail3@gmail.com",
                MailingGroupId = mailingGroup.Id
            });

            ApiRequestValidator.EnsureRequestSuccess<OkObjectResult>(actionResult);

            Assert.IsType<Guid>(((OkObjectResult)actionResult).Value);
        }

        [Fact]
        public async Task GetMailingEmails_MailingGroupHasMailingEmails_ShouldMailingList()
        {
            var mailingListDbContext = Host.Services.GetService<MailingListDbContext>();

            var user = mailingListDbContext.Users.First();
            _mailingEmailController.SetFakeHttpContextAndSetUser(user.Id);
            var mailingGroup = mailingListDbContext.MailingGroup.First(m => m.UserId == user.Id);

            var mailingEmailsRelatedToMailingGroup = mailingListDbContext.MailingEmailGroup
                .Include(meg => meg.MailingEmail)
                .Where(meg => meg.MailingGroupId == mailingGroup.Id)
                .ToList();

            var actionResult = await _mailingEmailController.GetMailingEmails(new GetMailingEmailListRequestModel()
            {
                MailingGroupId = mailingGroup.Id,
                Skip = 0,
                Take = 10
            });

            ApiRequestValidator.EnsureRequestSuccess<OkObjectResult>(actionResult);

            var mailingList = ((IEnumerable<MailingEmailModel>)((OkObjectResult)actionResult).Value).ToList();

            foreach (var mailingEmail in mailingList)
                Assert.Contains(mailingEmail.Id, mailingEmailsRelatedToMailingGroup.Select(meg => meg.MailingEmailId));
        }
    }
}
