using System;
using System.Threading.Tasks;

namespace BirthdayGreetings.App
{
    public class GreetingsApp : IDisposable
    {
        readonly SmtpGreetingsNotification smtpGreetingsNotification;
        readonly TextFileEmployeeCatalog employeeCatalog;

        public GreetingsApp(FileConfiguration fileConfiguration, SmtpConfiguration smtpConfiguration)
        {
            smtpGreetingsNotification = new SmtpGreetingsNotification(smtpConfiguration);
            employeeCatalog = new TextFileEmployeeCatalog(fileConfiguration);
        }

        public Task RunOnToday() =>
            Run(DateTime.Today);

        public async Task Run(DateTime today)
        {
            var allEmployees = await employeeCatalog.Load();
            
            var birthdayEmployees = 
                new BirthdayFilter(allEmployees)
                    .Apply(today);
            
            await smtpGreetingsNotification.SendBirthday(birthdayEmployees);
        }

        public void Dispose() =>
            smtpGreetingsNotification?.Dispose();
    }
}