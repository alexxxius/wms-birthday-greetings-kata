using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BirthdayGreetings.App
{
    public class SmtpGreetingsNotification : IDisposable
    {
        readonly SmtpConfiguration configuration;
        readonly ISendHooks hooks;
        readonly SmtpClient smtpClient;

        public SmtpGreetingsNotification(SmtpConfiguration configuration)
            : this(configuration, new NoHooks())
        {
        }

        public SmtpGreetingsNotification(SmtpConfiguration configuration, ISendHooks hooks)
        {
            this.configuration = configuration;
            this.hooks = hooks;
            smtpClient = new SmtpClient(configuration.Host, configuration.Port);
        }

        public async Task SendBirthday(IList<EmailInfo> infos)
        {
            foreach (var info in infos)
            {
                hooks.OnBefore();
                await smtpClient.SendMailAsync(configuration.Sender,
                    info.Email,
                    "Happy birthday!",
                    $"Happy birthday, dear {info.Name}!");
                hooks.OnAfter();
            }
        }

        public void Dispose() =>
            smtpClient?.Dispose();
    }

    public class NoHooks : ISendHooks
    {
        public void OnBefore()
        {
        }

        public void OnAfter()
        {
        }
    }

    public interface ISendHooks
    {
        void OnBefore();
        void OnAfter();
    }
}