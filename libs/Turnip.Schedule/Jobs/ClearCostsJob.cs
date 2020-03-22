using System.Threading.Tasks;
using Quartz;

namespace Turnip.Schedule
{
    public class ClearCostsJob : IJob
    {
        private readonly ITurnipService _turnip;

        public ClearCostsJob(ITurnipService turnip)
        {
            _turnip = turnip;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _turnip.DeleteCosts();
            return Task.CompletedTask;
        }
    }
}