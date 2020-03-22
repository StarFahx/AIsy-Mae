namespace Turnip.Discord
{
    public interface IRegisterHandler
    {
        string Register(string discordId, string[] parameters);
    }
}