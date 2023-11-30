using BasketballClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballClub.Services
{
    public interface ITeamService {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamByIdAsync(int id);
        Task<Team> CreateTeamAsync(Team team);
        Task UpdateTeamAsync(int id, Team team);
        Task DeleteTeamAsync(int id);
        Task<bool> IsTeamExistAsync(int id);
    }
}
