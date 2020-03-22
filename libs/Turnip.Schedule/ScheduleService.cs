using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Turnip.Schedule
{
    public class ScheduleService : IScheduleService
    {
        private IScheduler _scheduler;
        private readonly IJobFactory _jobFactory;

        public ScheduleService(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            
            // start scheduler
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            _scheduler = await factory.GetScheduler();
            _scheduler.JobFactory = _jobFactory;
            await _scheduler.Start();

            // clear costs every Sunday at noon
            IJobDetail clearCostsJob = JobBuilder.Create<ClearCostsJob>()
                .WithIdentity("clear costs")
                .Build();

            var sunday = DateTime.Today.AddDays(DaysTillSunday()).AddHours(12);
            ITrigger clearCostsTrigger = TriggerBuilder.Create()
                .WithIdentity("weekly")
                .StartAt(sunday)
                .WithSimpleSchedule(x => x.WithIntervalInHours(189).RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(clearCostsJob, clearCostsTrigger);

            // clear prices every day at midnight and midday
            IJobDetail clearPricesJob = JobBuilder.Create<ClearPricesJob>()
                .WithIdentity("clear prices")
                .Build();

            ITrigger clearPricesTrigger = TriggerBuilder.Create()
                .WithIdentity("twice daily")
                .StartAt(DateTime.Today)
                .WithSimpleSchedule(x => x.WithIntervalInHours(12).RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(clearPricesJob, clearPricesTrigger);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown();
        }

        private int DaysTillSunday()
        {
            DateTime today = DateTime.Now;
            switch (today.DayOfWeek)
            {
                case DayOfWeek.Monday: return 6;
                case DayOfWeek.Tuesday: return 5;
                case DayOfWeek.Wednesday: return 4;
                case DayOfWeek.Thursday: return 3;
                case DayOfWeek.Friday: return 2;
                case DayOfWeek.Saturday: return 1;
                case DayOfWeek.Sunday:
                default: return today.Hour < 12 ? 0 : 7;
            }
        }
    }
}