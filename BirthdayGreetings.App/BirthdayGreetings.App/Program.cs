using System.Configuration;
using System.Threading.Tasks;
using BirthdayGreetings.FileSystem;
using BirthdayGreetings.Smtp;

namespace BirthdayGreetings.App
{
    static class Program
    {
        static async Task Main()
        {
            var smtpConfiguration = SmtpConfiguration.From(ConfigurationManager.AppSettings);
            var fileConfiguration = FileConfiguration.From(ConfigurationManager.AppSettings);
            using var app = new GreetingsApp(fileConfiguration, smtpConfiguration);
            await app.RunOnToday();
        }
    }
}