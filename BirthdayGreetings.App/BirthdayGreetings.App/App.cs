using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BirthdayGreetings.App
{
    public class App
    {
        readonly FileConfiguration fileConfiguration;
        readonly SmtpConfiguration smtpConfiguration;

        public App(FileConfiguration fileConfiguration, SmtpConfiguration smtpConfiguration)
        {
            this.fileConfiguration = fileConfiguration;
            this.smtpConfiguration = smtpConfiguration;
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

            using (var smtpClient = new SmtpClient(smtpConfiguration.Host, smtpConfiguration.Port))
            {
                await smtpClient.SendMailAsync(smtpConfiguration.Sender,
                    email,
                    "Happy birthday!",
                    $"Happy birthday, dear {name}!");
            }
        }
    }
}