namespace Turnip
{
    public interface IPlayerBuilder
    {
        Player Build(string username, string discordID);
    }
}