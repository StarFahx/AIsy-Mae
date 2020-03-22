using Microsoft.Extensions.DependencyInjection;

namespace Turnip.Discord
{
    public class DiscordModule
    {
        public static DiscordModule Instance = new DiscordModule();

        private DiscordModule() { }

        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IBestHandler, BestHandler>();
            services.AddSingleton<ICostHandler, CostHandler>();
            services.AddSingleton<IPriceHandler, PriceHandler>();
            services.AddSingleton<IRegisterHandler, RegisterHandler>();
            services.AddSingleton<IDiscordService, DiscordService>();
            services.AddSingleton<IMessageReducer, MessageReducer>();
        }
    }
}