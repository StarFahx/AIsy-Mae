namespace Turnip.Discord
{
    public interface IPriceHandler
    {
        string HandlePrice(string discordID, string[] parameters);
    }
}