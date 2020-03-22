using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Turnip.Discord
{
    public class DiscordService : IDiscordService
    {
        private DiscordSocketClient _client;
        private readonly string _discordToken;
        private readonly IMessageReducer _messageReducer;

        public DiscordService(IMessageReducer messageReducer, IConfiguration configuration)
        {
            _discordToken = configuration.GetSection("Discord")["DiscordAPIKey"];
            _messageReducer = messageReducer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += _messageReducer.MessageReceived;
            await _client.LoginAsync(TokenType.Bot, _discordToken);
            await _client.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.StopAsync();
        }
    }
}