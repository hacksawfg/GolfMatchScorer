using GolfMatchScore.Server.Data;
using GolfMatchScore.Server.Models;
using GolfMatchScore.Shared.Models.Course;
using GolfMatchScore.Shared.Models.GolfRound;
using GolfMatchScore.Shared.Models.Player;
using GolfMatchScore.Shared.Models.Team;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Services.PlayerServices
{
    public class PlayerService : IPlayerService
    {
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;

        private readonly ApplicationDbContext _context;

        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePlayerAsync(PlayerCreate model)
        {
            var playerEntity = new Player
            {
                OwnerId = _userId,
                PlayerFirstName = model.PlayerFirstName,
                PlayerLastName = model.PlayerLastName
            };

            _context.Players.Add(playerEntity);
            var numChanges = await _context.SaveChangesAsync();
            return numChanges == 1;
        }

        public async Task<bool> DeletePlayerByIdAsync(int playerId)
        {
            var entity = await _context.Players.FindAsync(playerId);
            if (entity?.OwnerId != _userId)
                return false;

            _context.Players.Remove(entity);
            return await _context.SaveChangesAsync() == 1;

        }

        public async Task<PlayerDetails> GetPlayerDetailsByIdAsync(int playerId)
        {
            var playerQuery = await _context.Players
                .Include(t => t.Team)
                .Include(r => r.Rounds)
                .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(entity => entity.PlayerId == playerId);

            return playerQuery is null ? null : new PlayerDetails
            {
                PlayerId = playerQuery.PlayerId,
                PlayerFirstName = playerQuery.PlayerFirstName,
                PlayerLastName = playerQuery.PlayerLastName,
                Team = new TeamListItem
                {
                    TeamSchool = playerQuery.Team.TeamSchool
                },
                Rounds = playerQuery.Rounds.Select(r => new RoundListItem
                {
                    RoundId = r.RoundId,
                    MatchDate = r.MatchDate,
                    Course = new CourseListItem
                    {
                        CourseName = r.Course.CourseName
                    }

                }).ToList()
            };

        }

        public async Task<IEnumerable<PlayerListItem>> ListAllPlayersAsync()
        {
            var playerQuery = await _context.Players
                .Include(t => t.Team)
                .Select(n => new PlayerListItem
                {
                    PlayerId = n.PlayerId,
                    PlayerFirstName = n.PlayerFirstName,
                    PlayerLastName = n.PlayerLastName,
                    Team = new TeamListItem
                    {
                        TeamSchool = n.Team.TeamSchool
                    }
                }).ToListAsync();

            return playerQuery;
        }

        public async Task<bool> UpdatePlayerDetailsAsync(PlayerEdit model)
        {
            if (model == null)
                return false;

            var entity = await _context.Players.FindAsync(model.PlayerId);
            if (entity?.OwnerId != _userId)
                return false;

            entity.PlayerFirstName = model.PlayerFirstName;
            entity.PlayerLastName = model.PlayerLastName;
            entity.TeamId = model.TeamId;

            return await _context.SaveChangesAsync() == 1;

        }
    }
}
