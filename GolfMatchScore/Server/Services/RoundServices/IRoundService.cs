using GolfMatchScore.Shared.Models.GolfRound;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Services.RoundServices
{
    public interface IRoundService
    {
        Task<IEnumerable<RoundListItem>> ListAllRoundsAsync();
        Task<RoundDetails> GetRoundDetailsByIdAsync(int roundId);
        Task<bool> CreateRoundAsync(RoundCreate model);
        Task<bool> UpdateRoundById(RoundEdit model);
        Task<bool> DeleteRoundByIdAsync(int roundId);
        void SetUserId(string userId);
    }
}
