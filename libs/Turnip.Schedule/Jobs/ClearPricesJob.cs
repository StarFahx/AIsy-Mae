using System.Threading.Tasks;
using Quartz;

namespace Turnip.Schedule
{
    public class ClearPricesJob : IJob
    {
        private readonly ITurnipService _turnip;

        public ClearPricesJob(ITurnipService turnip)
        {
            _turnip = turnip;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _turnip.DeletePrices();
            return Task.CompletedTask;
        }
    }
}