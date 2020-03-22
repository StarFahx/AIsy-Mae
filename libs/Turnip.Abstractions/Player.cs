namespace Turnip
{
    public class Player
    {
        public Player() {}
        public Player(
            string name,
            string discordID
        )
        {
            Name = name;
            DiscordID = discordID;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DiscordID { get; set; }
        public int? Cost { get; set; }
        public int? Price { get; set; }
    }
}