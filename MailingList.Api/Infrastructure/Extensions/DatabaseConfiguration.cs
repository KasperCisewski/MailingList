using MailingList.Data;
using MailingList.Data.Domains;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MailingList.Api.Infrastructure.Extensions
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddScoped<DbContext, MailingListDbContext>();

            services
                .AddDbContext<MailingListDbContext>(options =>
                {
                    if (env.EnvironmentName == "Test")
                        options.UseInMemoryDatabase("TestDb");
                    else
                        options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
                });

            services.AddIdentity<User, IdentityRole<Guid>>(options =>
                    options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MailingListDbContext>();

            return services;
        }
    }
}
