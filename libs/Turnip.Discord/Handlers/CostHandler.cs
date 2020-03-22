namespace Turnip.Discord
{
    public class CostHandler : ICostHandler
    {
        private readonly ITurnipService _turnip;

        public CostHandler(ITurnipService turnip)
        {
            _turnip = turnip;
        }

        public string HandleCost(string discordID, string[] parameters)
        {
            string message;

            if (parameters.Length > 1 && int.TryParse(parameters[1], out int cost))
            {
                if (_turnip.SetCost(discordID, cost))
                {
                    message = string.Format("Thank you! I have set your cost for this week to {0} bells.", cost);
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