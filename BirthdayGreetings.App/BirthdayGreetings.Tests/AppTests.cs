using System;
using System.Globalization;
using System.Threading.Tasks;
using BirthdayGreetings.App;
using BirthdayGreetings.Tests.Support;
using FluentAssertions;
using netDumbster.smtp;
using Xunit;
using static BirthdayGreetings.Tests.Support.EmployeeFile;

namespace BirthdayGreetings.Tests
{
    public class AppTests : IDisposable
    {
        readonly FileConfiguration fileConfiguration = new FileConfiguration
        {
            FilePath = "employees.txt"
        };

        readonly SmtpConfiguration smtpConfiguration = new SmtpConfiguration
        {
            Sender = "foo@bar.com",
            Host = "localhost",
            Port = 5000
        };

        readonly SimpleSmtpServer smtpServer;
        readonly GreetingsApp app;

        public AppTests()
        {
            smtpServer = SimpleSmtpServer.Start(smtpConfiguration.Port);
            app = new GreetingsApp(fileConfiguration, smtpConfiguration);
        }

        public void Dispose()
        {
            app.Dispose();
            smtpServer.Dispose();
        }

        [Fact]
        public async Task NoSendsGreetingWhenNoBirthdays()
        {
            File(fileConfiguration.FilePath, Header(), Employee("Mary", "1982/11/08", "mary.ann@foobar.com"));

            await app.Run(Date("11/09/2020"));

            ReceivedMail.FromAll(smtpServer)
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task SendManyGreetingsWhenManyBirthdays()
        {
            File(fileConfiguration.FilePath,
                Header(),
                Employee("Matteo", "1982/09/11", "matteo@doubleloop.io"),
                Employee("John", "1982/10/08", "john.doe@foobar.com"),
                Employee("Mary", "1975/09/11", "mary.ann@foobar.com"));

            await app.Run(Date("11/09/2020"));

            ReceivedMail.FromAll(smtpServer)
                .Should()
                .BeEquivalentTo(
                    new ReceivedMail(smtpConfiguration.Sender,
                        "mary.ann@foobar.com",
                        "Happy birthday!",
                        "Happy birthday, dear Mary!"),
                    new ReceivedMail(smtpConfiguration.Sender,
                        "matteo@doubleloop.io",
                        "Happy birthday!",
                        "Happy birthday, dear Matteo!")
                );
        }

        static DateTime Date(String value) =>
            DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
}