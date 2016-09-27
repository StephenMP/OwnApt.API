using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using MySQL.Data.EntityFrameworkCore.Extensions;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Mapping;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo;
using OwnApt.Api.Repository.Sql.Core;
using OwnApt.Api.Repository.Sql.Lease;
using OwnApt.Authentication.Api.Filter;
using Swashbuckle.Swagger.Model;
using System;

namespace OwnApt.Api.AppStart
{
    public static class OwnAptStartup
    {
        #region Private Fields

        private static IConfigurationRoot Configuration;
        private static IHostingEnvironment HostEnvironment;

        #endregion Private Fields

        #region Public Methods

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

            AddMvc(app);
            AddSwagger(app);
        }

        public static void UseOwnAptServices(this IServiceCollection services)
        {
            AddFilters(services);
            AddAutoMapper(services);
            AddRepositories(services);
            AddServices(services);
            AddMongo(services);
            AddSql(services);
            AddSwagger(services);
            AddMemoryCache(services);
        }

        private static void AddMemoryCache(IServiceCollection services)
        {
            services.AddMemoryCache();
        }

        #endregion Public Methods

        #region Private Methods

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddSingleton<IMapper>(BuildMapper());
        }

        private static void AddFilters(IServiceCollection services)
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

        private static void AddMongo(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(BuildMongoClient());
        }

        private static void AddMvc(IApplicationBuilder app)
        {
            app.UseMvc();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IPropertyRepository, MongoPropertyRepository>();
            services.AddTransient<IOwnerRepository, MongoOwnerRepository>();
            services.AddTransient<ILeaseTermRepository, SqlLeaseTermRepository>();
            services.AddTransient<IRegisteredTokenRepository, SqlRegisteredTokenRepository>();
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

            services.AddDbContext<CoreContext>(options =>
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

        private static void AddSwagger(IApplicationBuilder app)
        {
            app.UseSwagger("api/{apiVersion}/info.json");
            app.UseSwaggerUi("api/v1/info", "/api/v1/info.json");
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

        #endregion Private Methods
    }
}
