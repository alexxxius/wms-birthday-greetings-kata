using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
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
            var lines = await File.ReadAllLinesAsync(fileConfiguration.FilePath);
            
            var employee = lines
                .Skip(1)
                
                .Select(line => line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray())
                
                .Select(parts => new
                {
                    Name = parts[1],
                    DateOfBirth = DateOfBirth.From(parts[2]),
                    Email = parts[3]
                })
                
                .Where(x => x.DateOfBirth.IsBirthday(today))
                
                .ToList();

            using var smtpClient = new SmtpClient(smtpConfiguration.Host, smtpConfiguration.Port);

            foreach (var e in employee)
            {
                await smtpClient.SendMailAsync(smtpConfiguration.Sender,
                    e.Email,
                    "Happy birthday!",
                    $"Happy birthday, dear {e.Name}!");
            }
        }
    }
}