using System;

namespace Turnip.Discord
{
    public class PriceHandler : IPriceHandler
    {
        private readonly ITurnipService _turnip;

        public PriceHandler(ITurnipService turnip)
        {
            _turnip = turnip;
        }

        public string HandlePrice(string discordID, string[] parameters)
        {
            string message;

            if (parameters.Length > 1 && int.TryParse(parameters[1], out int price))
            {
                if (_turnip.SetPrice(discordID, price))
                {
                    message = string.Format("Thank you! I have set your price for {0} {1} to {2} bells.", DateTime.Today.DayOfWeek, DateTime.Now.Hour <= 12 ? "Morning" : "Afternoon", price);
                }
                else
                {
                    message = "I'm sorry, I don't seem to have you registered. Please use the !register command to register your island.";
                }
            }
            else
            {
                message = "I'm sorry, your message seems to be poorly formed. Please see the !turnip-help command for guidance.";
            }

            return message;
        }
    }
}