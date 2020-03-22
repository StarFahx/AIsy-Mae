using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Turnip.Discord
{
    public class MessageReducer : IMessageReducer
    {
        private const string __helpMessage = @"```!turnip-help: Displays this message. But you already knew that! Heh...

!register [string:animal crossing username]: Registers you as that name. One-time only! Don't get it wrong!

!cost [int:bells per turnip]: Sets your town's turnip cost in bells.

!price [int:bells per turnip]: Sets your town's turnip price in bells.

!best: If it's a Sunday and Daisy Mae is about, displays a ranking of the best towns to visit to buy turnips. If it's not a Sunday and Nook's Corner is open, displays a ranking of the best towns to visit to sell turnips.```";
        private readonly IBestHandler _best;
        private readonly ICostHandler _cost;
        private readonly IPriceHandler _price;
        private readonly IRegisterHandler _register;

        public MessageReducer(
            IBestHandler best,
            ICostHandler cost,
            IPriceHandler price,
            IRegisterHandler register)
        {
            _best = best;
            _cost = cost;
            _price = price;
            _register = register;
        }

        public async Task MessageReceived(SocketMessage message)
        {
            var parameters = message.Content.Split(' ');
            var command = parameters[0].ToLower();
            if (command.StartsWith("!"))
            {
                string reply = string.Empty;
                switch (command)
                {
                    case "!turnip-help":
                        reply = __helpMessage;
                        break;
                    case "!cost":
                        reply = _cost.HandleCost(message.Author.Id.ToString(), parameters);
                        break;
                    case "!price":
                        reply = _price.HandlePrice(message.Author.Id.ToString(), parameters);
                        break;
                    case "!best":
                        reply = _best.GetBest(parameters);
                        break;
                    case "!register":
                        reply = _register.Register(message.Author.Id.ToString(), parameters);
                        break;
                }
                if (reply != string.Empty)
                {
                    await message.Channel.SendMessageAsync(reply);
                }
            }
        }

        
    }
}