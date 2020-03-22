using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Turnip;
using Turnip.Discord;
using Turnip.Schedule;

namespace ACTurnips
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new HostBuilder()
                .ConfigureAppConfiguration(builder => {
                    builder
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices(services => {
                    TurnipModule.Instance.AddServices(services);
                    DiscordModule.Instance.AddServices(services);
                    ScheduleModule.Instance.AddServices(services);
                })
                .ConfigureLogging(lb => {
                    lb.AddConsole();
                })
                .RunConsoleAsync();
        }
    }
}
