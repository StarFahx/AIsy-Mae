using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Turnip.Discord;
using Turnip.Schedule;

namespace Turnip
{
    public class TurnipHost : IHostedService
    {
        private readonly IDiscordService _discordService;
        private readonly IScheduleService _scheduleService;

        public TurnipHost(
            IDiscordService discordService,
            IScheduleService scheduleService)
        {
            _discordService = discordService;
            _scheduleService = scheduleService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _discordService.StartAsync(cancellationToken);
            await _scheduleService.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _discordService.StopAsync(cancellationToken);
            await _scheduleService.StopAsync(cancellationToken);
        }
    }
}