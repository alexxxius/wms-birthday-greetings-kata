using System;
using System.Threading.Tasks;
using BirthdayGreetings.Core;
using BirthdayGreetings.FileSystem;
using BirthdayGreetings.Smtp;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayGreetings.App
{
    // CompositionRoot + UseCase entry points
    public class GreetingsApp : IDisposable
    {
        readonly ServiceProvider serviceProvider;

        public GreetingsApp(FileConfiguration fileConfiguration, SmtpConfiguration smtpConfiguration)
        {
            var services = new ServiceCollection();
            
            services
                    
            .AddSingleton(new TextFileEmployeeCatalog(fileConfiguration))
            .AddSingleton<IEmployeeCatalog>(sp => sp.GetService<TextFileEmployeeCatalog>())
            
            .AddSingleton(new SmtpGreetingsNotification(smtpConfiguration))
            .AddSingleton<IGreetingsNotification>(sp => sp.GetService<SmtpGreetingsNotification>())
            
            .AddSingleton<IBirthdayService, DefaultBirthdayService>();
            
            serviceProvider = services.BuildServiceProvider();
        }

        public Task RunOnToday() => 
            Run(DateTime.Today);

        public Task Run(DateTime today)
        {
            var service = serviceProvider.GetService<IBirthdayService>();
            if (service == null)
                throw new InvalidOperationException();
            return service
                    .SendGreetings(today);
        }

        public void Dispose() => 
            serviceProvider?.Dispose();
    }
}