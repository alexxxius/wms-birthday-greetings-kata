using System;
using System.Net.Mail;
using netDumbster.smtp;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class AppTests
    {
        // - file one row yes birthday => one send

        [Fact]
        public void SendOneGreetingWhenOneBirthday()
        {
            var app = new App();

            using (var smtpServer = SimpleSmtpServer.Start(5000))
            {
                app.Run();

                var smtpMessage = Assert.Single(smtpServer.ReceivedEmail);
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
        public void Run()
        {
            using (var smtpClient = new SmtpClient("localhost", 5000))
            {
                smtpClient.Send("foo@bar.com", "mary.ann@foobar.com", "Happy birthday!", "Happy birthday, dear Mary!");
            }
        }
    }
}