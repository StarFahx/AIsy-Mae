using System.Threading;
using System.Threading.Tasks;

namespace Turnip.Discord
{
    public interface IDiscordService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}