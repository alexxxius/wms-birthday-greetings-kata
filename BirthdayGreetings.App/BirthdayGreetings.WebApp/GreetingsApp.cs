using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BirthdayGreetings.WebApp
{
    public class GreetingsApp
    {
        readonly IHost host;

        public GreetingsApp(string[] args) => 
            host = CreateHostBuilder(args).Build();

        public void Run() => 
            host.Run();

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}