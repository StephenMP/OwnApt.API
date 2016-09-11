﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OwnApt.Api.AppStart;

namespace Api
{
    public class Startup
    {
        #region Constructors

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables()
                            .Build();

            OwnAptStartup.ConfigureOwnAptStartup(Configuration, env);
        }

        #endregion Constructors

        #region Properties

        public IConfigurationRoot Configuration { get; }

        #endregion Properties

        #region Methods

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Configure app for OwnApt
            app.UseOwnAptConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure services for OwnApt
            services.UseOwnAptServices();
        }

        #endregion Methods
    }
}
