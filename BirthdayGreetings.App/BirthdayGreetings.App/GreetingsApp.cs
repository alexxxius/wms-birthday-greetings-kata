using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace BirthdayGreetings.App
{
    public class GreetingsApp
    {
        readonly FileConfiguration fileConfiguration;
        readonly SmtpConfiguration smtpConfiguration;

        public GreetingsApp(FileConfiguration fileConfiguration, SmtpConfiguration smtpConfiguration)
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
            var employee = noHeader
                .Select(line => line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                .Select(parts => new {Name = parts[1].Trim(), DateOfBirth = DateTime.Parse(parts[2].Trim()), Email = parts[3].Trim()})
                .Where(x => today.Month == x.DateOfBirth.Month && today.Day == x.DateOfBirth.Day)
                .ToList();

            foreach (var e in employee)
            {
                using (var smtpClient = new SmtpClient(smtpConfiguration.Host, smtpConfiguration.Port))
                {
                    await smtpClient.SendMailAsync(smtpConfiguration.Sender,
                        e.Email,
                        "Happy birthday!",
                        $"Happy birthday, dear {e.Name}!");
                }
            }
        }
    }
}