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
        // - file one row yes birthday => one send
        //        - hardcoded smtp endpoint in prod code
        //        - hardcoded from address in prod code
        //        - remember to add WhenOneBirthday to test name

        [Fact]
        public async Task SendOneGreeting()
        {
            var app = new App();

            using (var smtpServer = SimpleSmtpServer.Start(5000))
            {
                await app.Run();

                ReceivedMail.FromAll(smtpServer)
                    .Should()
                    .BeEquivalentTo(new ReceivedMail("foo@bar.com",
                        "mary.ann@foobar.com",
                        "Happy birthday!",
                        "Happy birthday, dear Mary!"));
            }
        }
    }

    public class App
    {
        public async Task Run()
        {
            using (var smtpClient = new SmtpClient("localhost", 5000))
            {
                await smtpClient.SendMailAsync("foo@bar.com",
                    "mary.ann@foobar.com",
                    "Happy birthday!",
                    "Happy birthday, dear Mary!");
            }
        }
    }
}