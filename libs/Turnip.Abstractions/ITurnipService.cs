using System.Collections.Generic;

namespace Turnip
{
    public interface ITurnipService
    {
        bool AddPlayer(string username, string discordID);
        void DeleteCosts();
        IEnumerable<Player> GetBestCost();
        void DeletePrices();
        IEnumerable<Player> GetBestPrice();
        bool SetCost(string discordID, int cost);
        bool SetPrice(string discordID, int price);
    }
}