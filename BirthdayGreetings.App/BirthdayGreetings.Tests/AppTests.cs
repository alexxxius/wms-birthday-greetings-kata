using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BirthdayGreetings.Tests.Support;
using FluentAssertions;
using netDumbster.smtp;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class AppTests
    {
        readonly String employeeTestFile = "employees.txt";
        
        // - file one row yes birthday => one send
        //        - hardcoded smtp endpoint in prod code
        //        - hardcoded from address in prod code
        //        - parsing con metodi ad-hoc

        [Fact]
        public async Task SendOneGreetingWhenOneBirthday()
        {
            EmployeeFile(employeeTestFile,
                Header(),
                Employee("Mary", "1975/09/11", "mary.ann@foobar.com")
            );
            
            var app = new App(new FileConfiguration
            {
                FilePath = employeeTestFile
            });

            using (var smtpServer = SimpleSmtpServer.Start(5000))
            {
                await app.Run(Date("11/09/2020"));

                ReceivedMail.FromAll(smtpServer)
                    .Should()
                    .BeEquivalentTo(new ReceivedMail("foo@bar.com",
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

    public class App
    {
        readonly FileConfiguration fileConfiguration;

        public App(FileConfiguration fileConfiguration)
        {
            this.fileConfiguration = fileConfiguration;
        }

        public Task RunOnToday() =>
            Run(DateTime.Today);
        public async Task Run(DateTime today)
        {
            var lines = File.ReadAllLines(fileConfiguration.FilePath);
            var noHeader = lines.Skip(1);
            var employee = noHeader.Single().Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            var email = employee[3];
            var name = employee[1].Trim();
            var date = DateTime.Parse(employee[2].Trim());

            using (var smtpClient = new SmtpClient("localhost", 5000))
            {
                await smtpClient.SendMailAsync("foo@bar.com",
                    email,
                    "Happy birthday!",
                    $"Happy birthday, dear {name}!");
            }
        }
    }
}