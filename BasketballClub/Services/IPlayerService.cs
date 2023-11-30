using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballClub.Models;

namespace BasketballClub.Services
{
    public interface IPlayerService {
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int id);
        Task<Player> CreatePlayerAsync(Player player);
        Task UpdatePlayerAsync(int id, Player player);
        Task DeletePlayerAsync(int id);
    }
}
