using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheBookIndex.Api.Configuration;
using TheBookIndex.Api.Filters;
using TheBookIndex.Data.EFCore;
using TheBookIndex.Data.Repositories;

namespace TheBookIndex.Api
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        private readonly ConfigurationManager _configurationManager;

        public Startup()
        {
            Console.WriteLine($"Environment: {ConfigurationManager.EnvironmentName}");
            try
            {
                _configurationManager = ConfigurationManager.BuildConfiguration(Directory.GetCurrentDirectory(),
                    ConfigurationManager.EnvironmentName);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong setting up config: {e.Message}");
                Console.WriteLine($"  Inner Exception: {e.InnerException?.Message}");

            }
            _configuration = _configurationManager.Configuration;
            Console.WriteLine("Configuration setup Complete");
        }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, DatabaseInitFilter>();

            services.AddControllers(setupActions =>
            {
                setupActions.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();

            services.AddSingleton(_configurationManager);

            AddDatabase(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddDatabase(IServiceCollection services)
        {
            services.AddSingleton(x => _configurationManager.ConnectionString);

            services.AddScoped<LibraryContext>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();

            //services.AddScoped<AuthenticationDbContext>();
        }
    }
}
