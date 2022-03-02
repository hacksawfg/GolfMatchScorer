using GolfMatchScore.Shared.Models.Team;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Services.TeamServices
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamListItem>> GetAllTeamsAsync();
        Task<TeamDetail> GetTeamDetailByIdAsync(int teamId);
        Task<bool> CreateTeamAsync(TeamCreate model);
        Task<bool> UpdateTeamByIdAsync(TeamEdit model);
        Task<bool> DeleteTeamByIdAsync(int teamId);
        void SetUserId(string userId);
    }
}
