using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using MySQL.Data.EntityFrameworkCore.Extensions;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Mapping;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo.Document;
using OwnApt.Api.Repository.Mongo.Core;
using OwnApt.Api.Repository.Mongo.Metadata;
using OwnApt.Api.Repository.Sql.Core;
using OwnApt.Api.Repository.Sql.Lease;
using OwnApt.Authentication.Api.Filter;
using Serilog;
using Swashbuckle.Swagger.Model;

namespace OwnApt.Api.AppStart
{
    public static class OwnAptStartup
    {
        #region Private Fields

        private static IConfigurationRoot Configuration;
        private static IHostingEnvironment HostEnvironment;

        #endregion Private Fields

        #region Public Methods

        public static void AddOwnAptServices(this IServiceCollection services)
        {
            AddMvc(services);
            AddAutoMapper(services);
            AddRepositories(services);
            AddServices(services);
            AddMongo(services);
            AddSql(services);
            AddSwagger(services);
            AddMemoryCache(services);
        }

        public static IMapper BuildMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MainProfile>();
            }).CreateMapper();
        }

        public static void ConfigureOwnAptStartup(IConfigurationRoot configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }

        public static void UseOwnAptConfiguration(this IApplicationBuilder app)
        {
            if (HostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            UseMvc(app);
            UseSwagger(app);
        }

        #endregion Public Methods

        #region Private Methods

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddSingleton<IMapper>(BuildMapper());
        }

        private static void AddMemoryCache(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
        }

        private static void AddMongo(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(BuildMongoClient());
            services.AddScoped<IMongoCoreContext, MongoCoreContext>();
            services.AddScoped<IMongoMetadataContext, MongoMetadataContext>();
            services.AddScoped<IMongoDocumentContext, MongoDocumentContext>();
        }

        private static void AddMvc(IServiceCollection services)
        {
            if (HostEnvironment.IsDevelopment())
            {
                services.AddMvc();
            }
            else
            {
                services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(HmacAuthenticationFilter));
                });
            }
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IPropertyRepository, MongoPropertyRepository>();
            services.AddTransient<IOwnerRepository, MongoOwnerRepository>();
            services.AddTransient<ILeaseTermRepository, SqlLeaseTermRepository>();
            services.AddTransient<IRegisteredTokenRepository, MongoRegisteredTokenRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<ILeaseTermService, LeaseTermService>();
            services.AddTransient<IRegisteredTokenService, RegisteredTokenService>();
        }

        private static void AddSql(IServiceCollection services)
        {
            var server = Configuration["SqlCore:Server"];
            var userID = Configuration["SqlCore:Uid"];
            var password = Configuration["SqlCore:Password"];

            var coreConnection = new MySqlConnectionStringBuilder
            {
                Server = server,
                UserID = userID,
                Password = password,
                SslMode = MySqlSslMode.None,
                Database = "Core"
            };

            services.AddDbContext<SqlCoreContext>(options =>
            {
                options.UseMySQL(coreConnection.ToString());
            });

            var leaseConnection = new MySqlConnectionStringBuilder
            {
                Server = server,
                UserID = userID,
                Password = password,
                SslMode = MySqlSslMode.None,
                Database = "Lease"
            };

            services.AddDbContext<LeaseContext>(options =>
            {
                options.UseMySQL(leaseConnection.ToString());
            });
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "OwnApt Core API",
                    Description = "All Things OwnApt"
                });
                //options.IncludeXmlComments(pathToDoc);
                options.DescribeAllEnumsAsStrings();
            });
        }

        private static MongoClient BuildMongoClient()
        {
            var coreDbName = Configuration["MongoCore:Name"];
            var username = Configuration["MongoCore:Username"];
            var password = Configuration["MongoCore:Password"];
            var host = Configuration["MongoCore:Host"];
            var port = Convert.ToInt32(Configuration["MongoCore:Port"]);

            var mongoClientSettings = new MongoClientSettings
            {
                Credentials = new MongoCredential[] { MongoCredential.CreateCredential(coreDbName, username, password) },
                ConnectionMode = ConnectionMode.Direct,
                Server = new MongoServerAddress(host, port)
            };

            return new MongoClient(mongoClientSettings);
        }

        private static void ConfigureLogging(ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            /* Serilog Configuration */
            var logentriesToken = Configuration["Logging:LogentriesToken"];
            var loggerConfig = new LoggerConfiguration()
                .Enrich.FromLogContext();

            if (HostEnvironment.IsDevelopment())
            {
                loggerConfig
                    .MinimumLevel.Information()
                    .WriteTo.Console();
            }
            else
            {
                loggerConfig
                    .MinimumLevel.Warning()
                    .WriteTo.Logentries(logentriesToken)
                    .WriteTo.Console();
                //.MinimumLevel.Information()
                //.WriteTo.RollingFile("logs\\DotCom-{Date}.txt")
                //.WriteTo.Logentries(logentriesToken, restrictedToMinimumLevel: LogEventLevel.Warning);
            }

            Log.Logger = loggerConfig.CreateLogger();

            loggerFactory.AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }

        private static void UseMvc(IApplicationBuilder app)
        {
            app.UseMvc();
        }

        private static void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger("api/{apiVersion}/info.json");
            app.UseSwaggerUi("api/v1/info", "/api/v1/info.json");
        }

        #endregion Private Methods
    }
}
