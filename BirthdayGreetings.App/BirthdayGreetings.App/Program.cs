using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BirthdayGreetings.App
{
    static class Program
    {
        static async Task Main()
        {
            var configuration = BuildConfiguration();

            using var app = new GreetingsAppBuilder()
                .WithEmployeeCatalog(x => x.FileSystem(configuration))         
                .WithGreetingsNotification(x => x.Smtp(configuration))         
                .Build();
            
            await app.RunOnToday();
        }

        static IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
    }

}