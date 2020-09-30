using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using BirthdayGreetings.App;
using BirthdayGreetings.Tests.Support;
using FluentAssertions;
using netDumbster.smtp;
using Xunit;

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

        /*
         * - namespace condivide il nome della classe
         * - duplicazione Trim(s) sui singoli valori parsati dal file
         * - parse file content
         * - check isBirthday
         * - send notifica
         * 
         */

        public AppTests() => 
            smtpServer = SimpleSmtpServer.Start(smtpConfiguration.Port);

        public void Dispose() => 
            smtpServer.Dispose();

        [Fact]
        public async Task SendOneGreetingWhenOneBirthday()
        {
            EmployeeFile(fileConfiguration.FilePath,
                Header(),
                Employee("Mary", "1975/09/11", "mary.ann@foobar.com")
            );
            var app = new App.App(fileConfiguration, smtpConfiguration);

            await app.Run(Date("11/09/2020"));

            ReceivedMail.FromAll(smtpServer)
                .Should()
                .BeEquivalentTo(new ReceivedMail(smtpConfiguration.Sender,
                    "mary.ann@foobar.com",
                    "Happy birthday!",
                    "Happy birthday, dear Mary!"));
        }

        [Fact]
        public async Task NoSendsGreetingWhenNoBirthdays()
        {
            EmployeeFile(fileConfiguration.FilePath,
                Header(),
                Employee("Mary", "1982/11/08", "mary.ann@foobar.com")
            );
            var app = new App.App(fileConfiguration, smtpConfiguration);

            await app.Run(Date("11/09/2020"));

            ReceivedMail.FromAll(smtpServer)
                .Should()
                .BeEmpty();
        }

        static void EmployeeFile(string fileName, params string[] lines) =>
            File.WriteAllLines(fileName, lines);

        static String Employee(String name, String date, String email) =>
            $"Ann, {name}, {date}, {email}";

        static String Header() =>
            "last_name, first_name, date_of_birth, email";

        static DateTime Date(String value) =>
            DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
}