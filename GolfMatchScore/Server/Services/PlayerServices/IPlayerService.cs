using GolfMatchScore.Shared.Models.Player;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Services.PlayerServices
{
    public interface IPlayerService
    {
        Task<bool> CreatePlayerAsync(PlayerCreate model);
        Task<IEnumerable<PlayerListItem>> ListAllPlayersAsync();
        Task<PlayerDetails> GetPlayerDetailsByIdAsync(int playerId);
        Task<bool> UpdatePlayerDetailsAsync(PlayerEdit model);
        Task<bool> DeletePlayerByIdAsync(int playerId);
        void SetUserId (string userId);
    }
}
