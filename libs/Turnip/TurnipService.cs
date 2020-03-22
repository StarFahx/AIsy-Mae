using System;
using System.Collections.Generic;
using System.Linq;

namespace Turnip
{
    public class TurnipService : ITurnipService
    {
        private readonly IPlayerBuilder _playerBuilder;
        private readonly IRepository<Player> _playerRepository;

        public TurnipService(
            IPlayerBuilder playerBuilder,
            IRepository<Player> playerRepository)
        {
            _playerBuilder = playerBuilder;
            _playerRepository = playerRepository;
        }

        public bool AddPlayer(string username, string discordID)
        {
            var player = _playerBuilder.Build(username, discordID);
            var valid = true;
            try
            {
                using (var data = _playerRepository.GetData())
                {
                    if (data.GetAll().Any(x => x.DiscordID == discordID))
                    {
                        valid = false;
                    }
                    else
                    {
                        data.Add(player);
                    }
                }
            }
            catch (Exception ex)
            {
                valid = false;
                Console.WriteLine(ex.Message);
            }
            return valid;
        }

        public void DeleteCosts()
        {
            try
            {
                using (var data = _playerRepository.GetData())
                {
                    foreach (var player in data.GetAll())
                    {
                        player.Cost = null;
                        data.Update(player);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<Player> GetBestCost()
        {
            try
            {
                using (var data = _playerRepository.GetData())
                {
                    return data.GetAll()
                        .Where(x => x.Cost.HasValue)
                        .OrderBy(x => x.Cost);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Player[0];
            }
        }

        public void DeletePrices()
        {
            try
            {
                using (var data = _playerRepository.GetData())
                {
                    foreach (var player in data.GetAll())
                    {
                        player.Price = null;
                        data.Update(player);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<Player> GetBestPrice()
        {
            try
            {
                using (var data = _playerRepository.GetData())
                {
                    return data.GetAll()
                        .Where(x => x.Price.HasValue)
                        .OrderByDescending(x => x.Price);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Player[0];
            }
        }

        public bool SetCost(string discordID, int cost)
        {
            try
            {
                using (var data = _playerRepository.GetData())
                {
                    var player = data.GetAll().FirstOrDefault(p => p.DiscordID == discordID);
                    var valid = player != null;
                    if (valid)
                    {
                        player.Cost = cost;
                        data.Update(player);
                    }
                    return valid;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool SetPrice(string discordID, int price)
        {
            try
            {
                using (var data = _playerRepository.GetData())
                {
                    var player = data.GetAll().FirstOrDefault(p => p.DiscordID == discordID);
                    var valid = player != null;
                    if (valid)
                    {
                        player.Price = price;
                        data.Update(player);
                    }
                    return valid;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}