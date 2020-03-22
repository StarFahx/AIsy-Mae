using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;

namespace Turnip.Schedule
{
    public class ScheduleModule
    {
        public static ScheduleModule Instance = new ScheduleModule();

        private ScheduleModule() { }

        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IScheduleService, ScheduleService>();
            services.AddSingleton<IJobFactory, JobFactory>();

            services.AddScoped<ClearCostsJob>();
            services.AddScoped<ClearPricesJob>();
        }
    }
}