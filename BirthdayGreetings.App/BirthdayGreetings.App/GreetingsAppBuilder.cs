using System;
using BirthdayGreetings.Core;
using BirthdayGreetings.FileSystem;
using BirthdayGreetings.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayGreetings.App
{
    // CompositionRoot
    public class GreetingsAppBuilder
    {
        readonly ServiceCollection serviceCollection;

        public GreetingsAppBuilder()
        {
            serviceCollection = new ServiceCollection();
        }

        public GreetingsAppBuilder WithEmployeeCatalog(Action<EmployeeCatalogExpression> configure)
        {
            var expression = new EmployeeCatalogExpression(serviceCollection);
            configure(expression);
            return this;
        }
        public GreetingsAppBuilder WithGreetingsNotification(Action<GreetingsNotificationExpression> configure)
        {
            var expression = new GreetingsNotificationExpression(serviceCollection);
            configure(expression);
            return this;
        }
        
        public GreetingsApp Build()=>
            new GreetingsApp(serviceCollection
                .AddCore()
                .BuildServiceProvider());
    }

    public class EmployeeCatalogExpression
    {
        readonly ServiceCollection serviceCollection;

        public EmployeeCatalogExpression(ServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        public void InMemory() =>
            serviceCollection.AddSingleton<IEmployeeCatalog, InMemoryEmployeeCatalog>();

        public void FileSystem(IConfiguration configuration) => 
            FileSystem(FileConfiguration.From(configuration));

        public void FileSystem(FileConfiguration fileConfiguration) => 
            serviceCollection.AddTextFileEmployeeCatalog(fileConfiguration);
    }

    public class GreetingsNotificationExpression
    {
        readonly ServiceCollection serviceCollection;

        public GreetingsNotificationExpression(ServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        public void InMemory()=>
            serviceCollection.AddSingleton<IGreetingsNotification, InMemoryGreetingsNotification>();

        public void Smtp(IConfiguration configuration) => 
            Smtp(SmtpConfiguration.From(configuration));

        public void Smtp(SmtpConfiguration smtpConfiguration) => 
            serviceCollection.AddSmtpGreetingsNotification(smtpConfiguration);
    }
}