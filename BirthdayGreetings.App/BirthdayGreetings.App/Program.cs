using System.Configuration;
using System.Threading.Tasks;

namespace BirthdayGreetings.App
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var smtpConfiguration = SmtpConfiguration.From(ConfigurationManager.AppSettings);
            var fileConfiguration = FileConfiguration.From(ConfigurationManager.AppSettings);
            
            using var app = new GreetingsApp(fileConfiguration, smtpConfiguration);

            await app.RunOnToday();
        }
    }
}