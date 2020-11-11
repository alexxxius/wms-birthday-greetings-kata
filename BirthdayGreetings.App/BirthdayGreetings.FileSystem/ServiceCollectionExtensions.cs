using BirthdayGreetings.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayGreetings.FileSystem
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTextFileEmployeeCatalog(this IServiceCollection services, FileConfiguration fileConfiguration) =>
            services
                .AddSingleton(new TextFileEmployeeCatalog(fileConfiguration))
                .AddSingleton<IEmployeeCatalog>(sp => sp.GetService<TextFileEmployeeCatalog>());
    }
}