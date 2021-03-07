using MailingList.Data.Repository;
using MailingList.Logic.Services;
using MailingList.Logic.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MailingList.Api.Infrastructure.Extensions
{
    public static class ComponentsInstallation
    {
        public static IServiceCollection RegisterComponents(this IServiceCollection services)
        {
            var domainAssembliesTypes = AppDomain.CurrentDomain.GetAssemblies()
                 .SelectMany(t => t.GetTypes());

            var repositoryTypesList = domainAssembliesTypes
                 .Where(t => t.IsClass && t.Namespace == "MailingList.Data.Repository.Implementation").ToArray();

            services.Scan(scan => scan
                 .FromAssembliesOf(repositoryTypesList)
                 .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
                 .AsImplementedInterfaces()
                 .WithTransientLifetime());

            services.AddTransient<IdentityValidator>();

            var serviceTypesList = domainAssembliesTypes
                .Where(t => t.IsClass && t.Namespace == "MailingList.Logic.Services.Implementation").ToArray();

            services.Scan(scan => scan
                .FromAssembliesOf(serviceTypesList)
                .AddClasses(classes => classes.AssignableTo(typeof(IService)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            var commandHandlerAndQueryHandlerTypes = domainAssembliesTypes
                .Where(t => t.IsClass && t.Namespace != null &&
                (t.Namespace.StartsWith("MailingList.Logic.CommandHandlers") || t.Namespace.StartsWith("MailingList.Logic.QueryHandlers")))
                .ToArray();

            services.AddMediatR(commandHandlerAndQueryHandlerTypes);

            return services;
        }
    }
}
