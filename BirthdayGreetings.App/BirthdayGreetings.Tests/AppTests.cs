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
    public class AppTests
    {
        readonly String employeeTestFile = "employees.txt";

        [Fact]
        public async Task SendOneGreetingWhenOneBirthday()
        {
            EmployeeFile(employeeTestFile,
                Header(),
                Employee("Mary", "1975/09/11", "mary.ann@foobar.com")
            );

            var smtpConfiguration = new SmtpConfiguration
            {
                Sender = "foo@bar.com",
                Host = "localhost",
                Port = 5000
            };
            var fileConfiguration = new FileConfiguration
            {
                FilePath = employeeTestFile
            };
            var app = new App.App(fileConfiguration, smtpConfiguration);

            using (var smtpServer = SimpleSmtpServer.Start(5000))
            {
                await app.Run(Date("11/09/2020"));

                ReceivedMail.FromAll(smtpServer)
                    .Should()
                    .BeEquivalentTo(new ReceivedMail(smtpConfiguration.Sender,
                        "mary.ann@foobar.com",
                        "Happy birthday!",
                        "Happy birthday, dear Mary!"));
            }
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