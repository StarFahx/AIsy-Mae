using Discord.WebSocket;

using System.Threading.Tasks;

namespace Turnip
{
    public interface IMessageReducer
    {
        Task MessageReceived(SocketMessage message);
    }
}