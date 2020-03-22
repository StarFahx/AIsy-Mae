namespace Turnip
{
    public class PlayerBuilder : IPlayerBuilder
    {
        public Player Build(string username, string discordID)
        {
            return new Player(username, discordID);
        }
    }
}