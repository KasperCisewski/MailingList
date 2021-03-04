using MailingList.Api.Infrastructure.Extensions;
using MailingList.Api.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MailingList.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc(
                    config =>
                    {
                        config.EnableEndpointRouting = false;
                    })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddControllersAsServices();
            services.AddJwtConfiguration(_configuration);

            if (_env.EnvironmentName != "Test")
                services.AddSwaggerConfiguration();

            services.AddControllers();
            services.AddDatabaseConfiguration(_configuration, _env);
            services.RegisterComponents();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            if (env.EnvironmentName != "Test")
            {
                var swaggerOptions = new SwaggerOptions();
                _configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

                app.UseSwagger(option => option.RouteTemplate = swaggerOptions.JsonRoute);
                app.UseSwaggerUI(option => option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description));
            }

            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
