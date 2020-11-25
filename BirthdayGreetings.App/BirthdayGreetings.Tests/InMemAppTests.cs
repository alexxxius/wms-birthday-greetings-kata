using System;
using System.Globalization;
using System.Threading.Tasks;
using BirthdayGreetings.App;
using BirthdayGreetings.FileSystem;
using BirthdayGreetings.Smtp;
using BirthdayGreetings.Tests.Support;
using FluentAssertions;
using netDumbster.smtp;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class InMemAppTests : IDisposable
    {
        readonly GreetingsApp app;

        public InMemAppTests()
        {
            app = new GreetingsAppBuilder()
                .WithEmployeeCatalog(x => x.InMemory())
                .WithGreetingsNotification(x => x.InMemory())
                .Build();
        }

        public void Dispose()
        {
            app.Dispose();
        }

        // [Fact]
        // public async Task NoSendsGreetingWhenNoBirthdays()
        // {
        //     EmployeeFile.File(fileConfiguration.FilePath, EmployeeFile.Header(), EmployeeFile.Employee("Mary", "1982/11/08", "mary.ann@foobar.com"));
        //
        //     await app.Run(Date("11/09/2020"));
        //
        //     ReceivedMail.FromAll(smtpServer)
        //         .Should()
        //         .BeEmpty();
        // }
        //
        // [Fact]
        // public async Task SendManyGreetingsWhenManyBirthdays()
        // {
        //     EmployeeFile.File(fileConfiguration.FilePath,
        //         EmployeeFile.Header(),
        //         EmployeeFile.Employee("Matteo", "1982/09/11", "matteo@doubleloop.io"),
        //         EmployeeFile.Employee("John", "1982/10/08", "john.doe@foobar.com"),
        //         EmployeeFile.Employee("Mary", "1975/09/11", "mary.ann@foobar.com"));
        //
        //     await app.Run(Date("11/09/2020"));
        //
        //     ReceivedMail.FromAll(smtpServer)
        //         .Should()
        //         .BeEquivalentTo(
        //             new ReceivedMail(smtpConfiguration.Sender,
        //                 "mary.ann@foobar.com",
        //                 "Happy birthday!",
        //                 "Happy birthday, dear Mary!"),
        //             new ReceivedMail(smtpConfiguration.Sender,
        //                 "matteo@doubleloop.io",
        //                 "Happy birthday!",
        //                 "Happy birthday, dear Matteo!")
        //         );
        // }

        static DateTime Date(String value) =>
            DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
}