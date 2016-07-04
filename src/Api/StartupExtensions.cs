using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OwnApt.Api.Domain.Mapping;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository;
using OwnApt.Api.Domain.Interface;

namespace OwnApt.Api
{
    public static class StartupExtensions
    {
        public static void AddOwnAptDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddInstance<IMongoClient>(new MongoClient(configuration["Mongo:ConnectionString"]));
            services.AddInstance<IMapper>(BuildMapper());

            services.AddTransient<IPropertyRepository, MongoPropertyRepository>();
            services.AddTransient<IPropertyService, PropertyService>();
        }

        public static IMapper BuildMapper()
        {
            return new MapperConfiguration(cfg =>
                cfg.AddProfile<PropertyProfile>()
            ).CreateMapper();
        }
    }
}