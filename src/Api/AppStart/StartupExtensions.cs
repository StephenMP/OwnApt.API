using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Mapping;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo;
using System;

namespace OwnApt.Api.AppStart
{
    public static class StartupExtensions
    {
        #region Public Methods

        public static void AddOwnAptDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddInstance<IMongoClient>(BuildMongoClient(configuration));
            services.AddInstance<IMapper>(BuildMapper());
            services.AddInstance<IConfigurationRoot>(configuration);

            services.AddTransient<IPropertyRepository, MongoPropertyRepository>();
            services.AddTransient<IPropertyService, PropertyService>();

            services.AddTransient<IPersonRepository, MongoPersonRepository>();
            services.AddTransient<IPersonService, PersonService>();
        }

        public static IMapper BuildMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PropertyProfile>();
                cfg.AddProfile<PersonProfile>();
            }).CreateMapper();
        }

        #endregion Public Methods

        #region Private Methods

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

        private static string BuildSqlCoreConnectionString(IConfigurationRoot configuration)
        {
            var server = configuration["SqlCore:Server"];
            var database = configuration["SqlCore:Database"];
            var uid = configuration["SqlCore:Uid"];
            var password = configuration["SqlCore:Password"];

            var connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            return connectionString;
        }

        #endregion Private Methods
    }
}
