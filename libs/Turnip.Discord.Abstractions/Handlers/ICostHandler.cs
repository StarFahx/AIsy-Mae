namespace Turnip.Discord
{
    public interface ICostHandler
    {
        string HandleCost(string discordID, string[] parameters);
    }
}