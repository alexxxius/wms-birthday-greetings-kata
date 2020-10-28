using System;
using System.Linq;
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

            var birthdayEmployees = allEmployees
                .Where(x => x.DateOfBirth.IsBirthday(today))
                .Select(x => x.EmailInfo)
                .ToList();

            await smtpGreetingsNotification.SendBirthday(birthdayEmployees);
        }

        public void Dispose() =>
            smtpGreetingsNotification?.Dispose();
    }

    public class Employee
    {
        public DateOfBirth DateOfBirth { get; }
        public EmailInfo EmailInfo { get; }

        public Employee(DateOfBirth dateOfBirth, EmailInfo emailInfo)
        {
            DateOfBirth = dateOfBirth;
            EmailInfo = emailInfo;
        }
    }
}