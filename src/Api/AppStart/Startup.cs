using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OwnApt.Api.AppStart;

namespace Api
{
    public class Startup
    {
        #region Public Constructors

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables()
                            .Build();
        }

        #endregion Public Constructors

        #region Public Properties

        public IConfigurationRoot Configuration { get; }

        #endregion Public Properties

        #region Public Methods

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Configure app for OwnApt
            app.UseOwnAptConfiguration(env);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure services for OwnApt
            services.UseOwnAptServices(Configuration);
        }

        #endregion Public Methods
    }
}
