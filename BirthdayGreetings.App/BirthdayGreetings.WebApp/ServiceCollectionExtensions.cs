using BirthdayGreetings.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayGreetings.WebApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services) =>
            services
                .AddSingleton<IBirthdayService, DefaultBirthdayService>();
    }
}