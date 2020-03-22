using System;
using System.Collections.Generic;

namespace Turnip.Discord
{
    public class BestHandler : IBestHandler
    {
        private readonly ITurnipService _turnip;
        
        public BestHandler(ITurnipService turnip)
        {
            _turnip = turnip;
        }

        public string GetBest(string[] parameters)
        {
            string reply = string.Empty;
            string type = string.Empty;
            Func<Player, int?> value = p => 0;
            IEnumerable<Player> players = null;

            if (DaisyMaePresent() || (parameters.Length > 1 && parameters[1].ToLower() == "buy"))
            {
                type = "cost";
                value = p => p.Cost;
                players = _turnip.GetBestCost();
            }
            else if (NooksCrannyOpen() || (parameters.Length > 1 && parameters[1].ToLower() == "sell"))
            {
                type = "sell for";
                value = p => p.Price;
                players = _turnip.GetBestPrice();
            }

            if (players != null)
            {
                int i = 1;
                bool first = true;
                foreach (var player in players)
                {
                    if (!first)
                    {
                        reply += "\n";
                    }
                    else
                    {
                        first = false;
                    }

                    reply += string.Format("{0}{1}. {2}'s turnips {3} {4} bells{0}", i == 1 ? "**" : "", i, player.Name, type, value(player));
                    i++;
                }
            }
            else
            {
                reply = "Sorry! Turnips are not available to buy or sell right now. Please check back later.";
            }

            return reply;
        }

        private bool DaisyMaePresent()
        {
            return DateTime.Today.DayOfWeek == DayOfWeek.Sunday
                && DateTime.Now.Hour < 12;
        }

        private bool NooksCrannyOpen()
        {
            return DateTime.Today.DayOfWeek != DayOfWeek.Sunday
                && DateTime.Now.Hour > 8
                && DateTime.Now.Hour < 22;
        }
    }
}