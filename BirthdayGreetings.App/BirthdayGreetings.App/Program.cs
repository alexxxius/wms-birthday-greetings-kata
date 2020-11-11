using System.Threading.Tasks;
using BirthdayGreetings.FileSystem;
using BirthdayGreetings.Smtp;
using Microsoft.Extensions.Configuration;

namespace BirthdayGreetings.App
{
    static class Program
    {
        static async Task Main()
        {
            var configuration = BuildConfiguration();
            var smtpConfiguration = SmtpConfiguration.From(configuration);
            var fileConfiguration = FileConfiguration.From(configuration);

            using var app = new GreetingsApp(fileConfiguration, smtpConfiguration);
            await app.RunOnToday();
        }

        static IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
    }
}