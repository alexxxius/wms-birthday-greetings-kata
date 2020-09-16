using System;
using System.Net.Mail;
using System.Threading.Tasks;
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
        //        - troppe assert
        //
        // confrontare intero oggetto StmpMessage[] + 2
        // uso Linq Single e tolgo 3 assert + 1
        // extract utility method AssertMessage(from, to, s, b, smtpMessage) 

        [Fact]
        public async  Task SendOneGreeting()
        {
            var app = new App();

            using (var smtpServer = SimpleSmtpServer.Start(5000))
            {
                await app.Run();

                // ne ho ricevuta 1
                var smtpMessage = Assert.Single(smtpServer.ReceivedEmail);

                // quella ricevuta ha la forma che mi aspetto
                Assert.Equal("foo@bar.com", smtpMessage.FromAddress.Address);
                var toAddress = Assert.Single(smtpMessage.ToAddresses);
                Assert.Equal("mary.ann@foobar.com", toAddress.Address);
                Assert.Equal("Happy birthday!", smtpMessage.Headers["Subject"]);
                var messagePart = Assert.Single(smtpMessage.MessageParts);
                Assert.Equal("Happy birthday, dear Mary!", messagePart.BodyData);
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