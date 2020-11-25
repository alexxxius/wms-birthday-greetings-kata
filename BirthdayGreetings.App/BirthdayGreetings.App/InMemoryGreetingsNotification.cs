using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayGreetings.Core;

namespace BirthdayGreetings.App
{
    public class InMemoryGreetingsNotification : IGreetingsNotification
    {
        public InMemoryGreetingsNotification()
        {
            EmailSent = new ConcurrentBag<EmailInfo>();
        }

        public ConcurrentBag<EmailInfo> EmailSent { get; set; }

        public Task SendBirthday(IList<EmailInfo> infos) =>
            Task.Delay(10)
                .ContinueWith(_ => infos.ToList().ForEach(EmailSent.Add));
    }
}