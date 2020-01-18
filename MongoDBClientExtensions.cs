using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctions.MongoDBDriver
{
    public static class MongoDBClientExtensions
    {

        public static IServiceCollection AddMongoDBClient(this IServiceCollection services)
        {

            services.AddSingleton<IMongoDBClient, MongoDBClient>();

            services.AddOptions<DatabaseConnectionOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.Bind(settings);
                });

            return services;
        }
    }
}
