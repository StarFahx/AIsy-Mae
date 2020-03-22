using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Turnip
{
    public class TurnipModule
    {
        public static TurnipModule Instance = new TurnipModule();

        private TurnipModule() { }

        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IHostedService, TurnipHost>();
            services.AddSingleton<ITurnipService, TurnipService>();
            services.AddSingleton<IRepository<Player>, Repository>();
            services.AddSingleton<IPlayerBuilder, PlayerBuilder>();
        }
    }
}