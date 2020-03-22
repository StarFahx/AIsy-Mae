using System.Threading;
using System.Threading.Tasks;

namespace Turnip.Schedule
{
    public interface IScheduleService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}