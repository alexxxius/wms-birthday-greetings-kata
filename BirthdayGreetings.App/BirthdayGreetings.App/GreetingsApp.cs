using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayGreetings.App
{
    public class GreetingsApp : IDisposable
    {
        readonly FileConfiguration fileConfiguration;
        readonly SmtpGreetingsNotification smtpGreetingsNotification;

        public GreetingsApp(FileConfiguration fileConfiguration, SmtpConfiguration smtpConfiguration)
        {
            this.fileConfiguration = fileConfiguration;
            smtpGreetingsNotification = new SmtpGreetingsNotification(smtpConfiguration);
        }

        public Task RunOnToday() =>
            Run(DateTime.Today);

        public async Task Run(DateTime today)
        {
            var lines = await File.ReadAllLinesAsync(fileConfiguration.FilePath);
            
            var employee = lines
                .Skip(1)
                
                .Select(line => line
                    .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray())
                
                .Select(parts => new
                {
                    DateOfBirth = DateOfBirth.From(parts[2]),
                    Info = new EmailInfo(parts[1], parts[3])
                })
                .Where(x => x.DateOfBirth.IsBirthday(today))
                .Select(x => x.Info)
                .ToList();

            await smtpGreetingsNotification.SendBirthday(employee);
        }

        public void Dispose() => 
           smtpGreetingsNotification?.Dispose();
    }
}