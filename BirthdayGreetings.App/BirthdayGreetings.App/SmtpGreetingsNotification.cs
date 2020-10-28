using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BirthdayGreetings.App
{
    public class SmtpGreetingsNotification : IDisposable
    {
        readonly SmtpConfiguration configuration;
        readonly SmtpClient smtpClient;

        public SmtpGreetingsNotification(SmtpConfiguration configuration)
        {
            this.configuration = configuration;
            smtpClient = new SmtpClient(configuration.Host, configuration.Port);
        }

        public async Task SendBirthday(IList<EmailInfo> infos)
        {
            foreach (var info in infos)
                await smtpClient.SendMailAsync(configuration.Sender,
                    info.Email,
                    "Happy birthday!",
                    $"Happy birthday, dear {info.Name}!");
        }

        public void Dispose() => 
            smtpClient?.Dispose();
    }
}