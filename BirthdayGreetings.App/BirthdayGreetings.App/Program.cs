using System.Configuration;
using System.Threading.Tasks;

namespace BirthdayGreetings.App
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var smtpConfiguration = SmtpConfiguration.From(ConfigurationManager.AppSettings);
            var fileConfiguration = new FileConfiguration
            {
                FilePath = "employees-data.txt"
            };
            var app = new GreetingsApp(fileConfiguration, smtpConfiguration);

            await app.RunOnToday();
        }
    }
}
