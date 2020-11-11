using BirthdayGreetings.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayGreetings.Smtp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmtpGreetingsNotification(this IServiceCollection services, SmtpConfiguration configuration) =>
            services
                .AddSingleton(new SmtpGreetingsNotification(configuration))
                .AddSingleton<IGreetingsNotification>(sp => sp.GetService<SmtpGreetingsNotification>());
    }
}