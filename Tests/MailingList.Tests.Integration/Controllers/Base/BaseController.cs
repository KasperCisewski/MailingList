using MailingList.Api;
using MailingList.Data;
using MailingList.Tests.Integration.Infrastructure.Seeder;
using MailingList.Tests.Integration.Infrastructure.Validators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MailingList.Tests.Integration.Controllers.Base
{
    public class BaseController : IDisposable
    {
        protected TestServer Server;
        protected IWebHost Host;
        protected ApiRequestValidator ApiRequestValidator;

        public BaseController()
        {
            Task.WaitAll(Task.Run(async () =>
            {
                Server = new TestServer(new WebHostBuilder()
                            .UseEnvironment("Test")
                            .UseStartup<Startup>()
                            );
                Host = Server.Host;
                ApiRequestValidator = new ApiRequestValidator();
                var basicDataSeeder = new BasicDataSeeder(Host, ApiRequestValidator);
                await basicDataSeeder.SeedBasicDataViaApi();
            }));
        }

        public void Dispose()
        {
            var dbContext = Host.Services.GetService<MailingListDbContext>();
            dbContext.Database.EnsureDeleted();
            Host?.Dispose();
            Server?.Dispose();
        }
    }
}
