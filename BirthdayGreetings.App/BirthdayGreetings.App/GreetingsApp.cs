using System;
using System.Threading.Tasks;
using BirthdayGreetings.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayGreetings.App
{
    // Composed App + UseCase entry points
    public class GreetingsApp : IDisposable
    {
        readonly ServiceProvider serviceProvider;

        public GreetingsApp(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
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