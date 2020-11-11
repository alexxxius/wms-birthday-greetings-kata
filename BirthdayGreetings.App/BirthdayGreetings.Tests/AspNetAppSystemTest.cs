using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class AspNetAppSystemTest
    {
        [Fact(Skip = "not fully implemented")]
        public async Task Run()
        {
            var server = Task.Run(() =>
            {
                var greetingsApp = new WebApp.GreetingsApp(new[]
                {
                    "AspNetCore__ConnectionStrings_MongoDB=..."
                });
                greetingsApp.Run();
            });

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("...");
            response.EnsureSuccessStatusCode();

            await server;
        }
    }
}