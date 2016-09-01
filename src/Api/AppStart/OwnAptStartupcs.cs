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
using OwnApt.Api.Repository.Sql;
using OwnApt.Authentication.Api.Filter;
using Swashbuckle.Swagger.Model;
using System;

namespace OwnApt.Api.AppStart
{
    public static class OwnAptStartupcs
    {
        #region Public Methods

        public static IMapper BuildMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PropertyProfile>();
                cfg.AddProfile<PersonProfile>();
                cfg.AddProfile<UserLoginProfile>();
            }).CreateMapper();
        }

        public static void UseOwnAptConfiguration(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AddMvc(app);
            AddSwagger(app);
        }

        public static void UseOwnAptServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            AddFilters(services);
            AddAutoMapper(services);
            AddRepositories(services);
            AddServices(services);
            AddMongo(services, configuration);
            AddMySql(services, configuration);
            AddSwagger(services);
        }

        #endregion Public Methods

        #region Private Methods

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddSingleton<IMapper>(BuildMapper());
        }

        private static void AddFilters(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HmacAuthenticationFilter));
            });
        }

        private static void AddMongo(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSingleton<IMongoClient>(BuildMongoClient(configuration));
        }

        private static void AddMvc(IApplicationBuilder app)
        {
            app.UseMvc();
        }

        private static void AddMySql(IServiceCollection services, IConfigurationRoot configuration)
        {
            var server = configuration["SqlCore:Server"];
            var database = configuration["SqlCore:Database"];
            var userID = configuration["SqlCore:Uid"];
            var password = configuration["SqlCore:Password"];

            var mysqlConnectionBuilder = new MySqlConnectionStringBuilder
            {
                Server = server,
                Database = database,
                UserID = userID,
                Password = password,
                SslMode = MySqlSslMode.None
            };

            services.AddDbContext<CoreContext>(options =>
            {
                options.UseMySQL(mysqlConnectionBuilder.ToString());
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IPropertyRepository, MongoPropertyRepository>();
            services.AddTransient<IPersonRepository, MongoPersonRepository>();
            services.AddTransient<IUserLoginRepository, UserLoginRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IUserLoginService, UserLoginService>();
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

        private static MongoClient BuildMongoClient(IConfigurationRoot configuration)
        {
            var coreDbName = configuration["MongoCore:Name"];
            var username = configuration["MongoCore:Username"];
            var password = configuration["MongoCore:Password"];
            var host = configuration["MongoCore:Host"];
            var port = Convert.ToInt32(configuration["MongoCore:Port"]);

            var mongoClientSettings = new MongoClientSettings()
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
