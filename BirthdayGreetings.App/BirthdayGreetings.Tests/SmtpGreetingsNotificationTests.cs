using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using BirthdayGreetings.App;
using BirthdayGreetings.Tests.Support;
using FluentAssertions;
using netDumbster.smtp;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class SmtpGreetingsNotificationTests
    {
        readonly SmtpConfiguration smtpConfiguration = new SmtpConfiguration
        {
            Sender = "foo@bar.com",
            Host = "localhost",
            Port = 5001
        };

        [Fact]
        public async Task SendBirthday()
        {
            using var smtpServer = SimpleSmtpServer.Start(smtpConfiguration.Port);
            using var notification = new SmtpGreetingsNotification(smtpConfiguration);

            await notification.SendBirthday(new[]
            {
                new EmailInfo("foo", "a@a.com"),
            });

            ReceivedMail.FromAll(smtpServer)
                .Should()
                .BeEquivalentTo(new ReceivedMail(smtpConfiguration.Sender,
                    "a@a.com",
                    "Happy birthday!",
                    "Happy birthday, dear foo!"));
        }

        [Fact]
        public async Task ServerUnreachableOnFirstSend()
        {
            using var smtpServer = SimpleSmtpServer.Start(smtpConfiguration.Port);
            using var notification = new SmtpGreetingsNotification(smtpConfiguration);

            smtpServer.Stop();

            var ex = await Assert.ThrowsAsync<SmtpException>(() =>
                notification.SendBirthday(new[]
                {
                    new EmailInfo("foo", "a@a.com"),
                }));
            Assert.Equal(SmtpStatusCode.GeneralFailure, ex.StatusCode);
        }

        [Fact]
        public async Task ServerUnreachableDuringSend()
        {
            using var smtpServer = SimpleSmtpServer.Start(smtpConfiguration.Port);
            using var notification = new SmtpGreetingsNotification(smtpConfiguration,
                new StopServerAfterOneSend(smtpServer));

            var ex = await Assert.ThrowsAsync<SmtpException>(() => 
                notification.SendBirthday(new[]
                {
                    new EmailInfo("foo", "a@a.com"),
                    new EmailInfo("bar", "b@b.com"),
                }));
            Assert.Equal(SmtpStatusCode.GeneralFailure, ex.StatusCode);
            
            ReceivedMail.FromAll(smtpServer)
                .Should()
                .BeEquivalentTo(new ReceivedMail(smtpConfiguration.Sender,
                    "a@a.com",
                    "Happy birthday!",
                    "Happy birthday, dear foo!"));
        }

        [Fact]
        public void ServerUnreachable()
        {
            // NOTE: should not throws any SmtpException 
            using var notification = new SmtpGreetingsNotification(smtpConfiguration);
        }

        class StopServerAfterOneSend : ISendHooks
        {
            readonly SimpleSmtpServer smtpServer;
            bool isFirstSend;

            public StopServerAfterOneSend(SimpleSmtpServer smtpServer)
            {
                this.smtpServer = smtpServer;
                isFirstSend = true;
            }

            public void OnBefore()
            {
                if (!isFirstSend)
                    smtpServer.Stop();
            }

            public void OnAfter()
            {
                if (isFirstSend)
                    isFirstSend = false;
            }
        }
    }
}