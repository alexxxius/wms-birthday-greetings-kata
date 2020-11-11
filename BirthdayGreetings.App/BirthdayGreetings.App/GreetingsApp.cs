using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayGreetings.Core;
using BirthdayGreetings.FileSystem;
using BirthdayGreetings.MongoDb;
using BirthdayGreetings.Smtp;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

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
                .AddSingleton<IEmployeeCatalog>(sp => new TextFileEmployeeCatalog(fileConfiguration))
                .AddSingleton<IGreetingsNotification>(sp => new SmtpGreetingsNotification(smtpConfiguration))
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