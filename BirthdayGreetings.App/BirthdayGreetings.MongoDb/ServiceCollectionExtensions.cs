using BirthdayGreetings.Core;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BirthdayGreetings.MongoDb
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTextFileEmployeeCatalog(this IServiceCollection services, string connectionString)=>
            services
                .AddSingleton(MongoDbBootstrapper.Init(connectionString))
                .AddSingleton(sp => new MongoEmployeeCatalog(sp.GetService<IMongoDatabase>()))
                .AddSingleton<IEmployeeCatalog>(sp => sp.GetService<MongoEmployeeCatalog>());
    }
}