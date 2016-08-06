using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OwnApt.Api.AppStart;
using OwnApt.Authentication.Api.Filter;

namespace OwnApt.Api
{
    public class Startup
    {
        #region Public Fields + Properties

        private bool IsDevelopmentEnvironment;
        public IConfigurationRoot Configuration { get; set; }

        #endregion Public Fields + Properties

        #region Public Constructors + Destructors

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            this.IsDevelopmentEnvironment = env.IsDevelopment();

            if (this.IsDevelopmentEnvironment)
            {
                builder.AddJsonFile("appSettings_Dev.json");
            }
            else
            {
                builder.AddJsonFile("appsettings.json");
            }

            Configuration = builder.Build();
        }

        #endregion Public Constructors + Destructors

        #region Public Methods

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            if (this.IsDevelopmentEnvironment)
            {
                services.AddMvc();
            }

            else
            {
                services.AddMvc(c =>
                    c.Filters.Add(new AuthenticationFilter())
                );
            }

            services.AddRouting();
            services.AddOwnAptDependencies(Configuration);
        }

        #endregion Public Methods
    }
}
