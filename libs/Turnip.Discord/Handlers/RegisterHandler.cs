namespace Turnip.Discord
{
    public class RegisterHandler : IRegisterHandler
    {
        private readonly ITurnipService _turnip;
        
        public RegisterHandler(ITurnipService turnip)
        {
            _turnip = turnip;
        }

        public string Register(string discordId, string[] parameters)
        {
            string message = string.Empty;

            if (parameters.Length > 1)
            {
                if (_turnip.AddPlayer(parameters[1], discordId))
                {
                    message = "Thank you for registering!";
                }
                else
                {
                    message = "You're already registered! You don't need to do that again.";
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