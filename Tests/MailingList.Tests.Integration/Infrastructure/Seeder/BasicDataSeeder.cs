using MailingList.Api.Controllers;
using MailingList.Api.Models.Requests.Identity;
using MailingList.Api.Models.Requests.MailingGroup;
using MailingList.Api.Models.Requests.MailingGroupEmail;
using MailingList.Data;
using MailingList.Tests.Integration.Infrastructure.Extensions;
using MailingList.Tests.Integration.Infrastructure.Validators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailingList.Tests.Integration.Infrastructure.Seeder
{
    internal class BasicDataSeeder
    {
        private readonly IWebHost _host;
        private readonly ApiRequestValidator _apiRequestValidator;

        public BasicDataSeeder(IWebHost Host, ApiRequestValidator apiRequestValidator)
        {
            _host = Host;
            _apiRequestValidator = apiRequestValidator;
        }

        public async Task SeedBasicDataViaApi()
        {
            var mailingListDbContext = _host.Services.GetService<MailingListDbContext>();
            await RegisterTestUser();
            var user = await mailingListDbContext.Users.FirstOrDefaultAsync();
            if (user == null)
                throw new Exception("Could not found any user in db context. registration was failed");

            var mailingGroupId = await AddMailingGroupToUser(user.Id);
            await AddedMailingEmailsForGroup(user.Id, mailingGroupId);
        }

        private async Task RegisterTestUser()
        {
            var identityController = _host.Services.GetService<IdentityController>();

            var actionResult = await identityController.Register(new UserRegistrationRequest()
            {
                Email = "test123@gmail.com",
                Password = "Test123!",
                Username = "test123"
            });

            _apiRequestValidator.EnsureRequestSuccess<OkObjectResult>(actionResult);
        }

        private async Task<Guid> AddMailingGroupToUser(Guid userId)
        {
            var mailingGroupController = _host.Services.GetService<MailingGroupController>();
            mailingGroupController.SetFakeHttpContextAndSetUser(userId);
            var actionResult = await mailingGroupController.Create(new MailingGroupRequestModel()
            {
                Name = "testMailingGroup"
            });

            _apiRequestValidator.EnsureRequestSuccess<OkObjectResult>(actionResult);

            return (Guid)((OkObjectResult)actionResult).Value;
        }

        private async Task AddedMailingEmailsForGroup(Guid userId, Guid mailingGroupId)
        {
            var emails = new List<string>()
            {
                "testEmail1@gmail.com",
                "testEmail2@gmail.com"
            };

            var mailingEmailController = _host.Services.GetService<MailingEmailController>();
            mailingEmailController.SetFakeHttpContextAndSetUser(userId);

            foreach (var email in emails)
            {
                var actionResult = await mailingEmailController.Create(new MailingEmailRequestModel()
                {
                    Email = email,
                    MailingGroupId = mailingGroupId
                });

                _apiRequestValidator.EnsureRequestSuccess<OkObjectResult>(actionResult);
            }
        }
    }
}
