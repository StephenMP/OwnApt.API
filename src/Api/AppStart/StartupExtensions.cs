using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Mapping;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository;
using OwnApt.Api.Repository.Interface;
using System;

namespace OwnApt.Api.AppStart
{
    public static class StartupExtensions
    {
        public static void AddOwnAptDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddInstance<IMongoClient>(BuildMongoClient(configuration));
            services.AddInstance<IMapper>(BuildMapper());

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

        private static MongoClient BuildMongoClient(IConfigurationRoot configuration)
        {
            var coreDbName = configuration["MongoCore:Name"];
            var username = configuration["MongoCore:Username"];
            var password = configuration["MongoCore:Password"];
            var host = configuration["MongoCore:Host"];
            var port = configuration["MongoCore:Port"];

            var mongoClientSettings = new MongoClientSettings()
            {
                Credentials = new MongoCredential[] { MongoCredential.CreateCredential(coreDbName, username, password) },
                ConnectionMode = ConnectionMode.Direct,
                Server = new MongoServerAddress(host, Convert.ToInt32(port))
            };

            return new MongoClient(mongoClientSettings);
        }
    }
}