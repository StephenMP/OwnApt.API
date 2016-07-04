using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace OwnApt.Api
{
    public static class StartupExtensions
    {
        public static void AddOwnAptDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddInstance<IMongoClient>(new MongoClient(configuration.Get("Mongo:ConnectionString")));
        }
    }
}