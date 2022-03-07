using GolfMatchScore.Server.Data;
using GolfMatchScore.Server.Models;
using GolfMatchScore.Shared.Models.GolfRound;
using GolfMatchScore.Shared.Models.Player;
using GolfMatchScore.Shared.Models.Team;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Services.RoundServices
{
    public class RoundService : IRoundService
    {
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;

        private readonly ApplicationDbContext _context;
        public RoundService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRoundAsync(RoundCreate model)
        {
            var roundEntity = new GolfRound
            {
                OwnerId = _userId,
                MatchDate = model.MatchDate,
                MatchScore = model.MatchScore,
                CourseId = model.CourseId,
                PlayerId = model.PlayerId
            };

            _context.GolfRounds.Add(roundEntity);
            var numChanges = await _context.SaveChangesAsync();
            return numChanges == 1;
        }

        public async Task<bool> DeleteRoundByIdAsync(int roundId)
        {
            var entity = await _context.GolfRounds.FindAsync(roundId);
            if (entity?.OwnerId != _userId)
                return false;

            _context.GolfRounds.Remove(entity);
            var numChanges = await _context.SaveChangesAsync();
            return numChanges == 1;
        }

        public async Task<RoundDetails> GetRoundDetailsByIdAsync(int roundId)
        {
            var roundQuery = await _context.GolfRounds
                .Include(c => c.Course)
                .Include(p => p.Player)
                .ThenInclude(t => t.Team)
                .FirstOrDefaultAsync(entity => entity.RoundId == roundId);

            return roundQuery is null ? null : new RoundDetails
            {
                RoundId = roundQuery.RoundId,
                MatchDate = roundQuery.MatchDate,
                MatchScore = roundQuery.MatchScore,
                Player = new PlayerDetails
                {
                    PlayerId = roundQuery.Player.PlayerId,
                    PlayerFirstName = roundQuery.Player.PlayerFirstName,
                    PlayerLastName = roundQuery.Player.PlayerLastName,
                    Team = new TeamListItem
                    {
                        TeamId = roundQuery.Player.Team.TeamId,
                        TeamSchool = roundQuery.Player.Team.TeamSchool
                    }
                },
                Course = new Shared.Models.Course.CourseDetail
                {
                    CourseId = roundQuery.Course.CourseId,
                    CourseName = roundQuery.Course.CourseName,
                    CourseCity = roundQuery.Course.CourseCity,
                    CourseState = roundQuery.Course.CourseState
                }
            };
        }

        public async Task<IEnumerable<RoundListItem>> ListAllRoundsAsync()
        {
            var roundQuery = await _context.GolfRounds
                .Include(c => c.Course)
                .Include(p => p.Player)
                .Select(n => new RoundListItem
                {
                    RoundId = n.RoundId,
                    MatchDate = n.MatchDate,
                    Course = new Shared.Models.Course.CourseListItem
                    {
                        CourseName = n.Course.CourseName
                    },
                    Player = new PlayerListItem
                    {
                        PlayerFirstName = n.Player.PlayerFirstName,
                        PlayerLastName = n.Player.PlayerLastName
                    }
                }).ToListAsync();

            return roundQuery;
        }

        public async Task<bool> UpdateRoundById(RoundEdit model)
        {
            if (model == null)
                return false;

            var entity = await _context.GolfRounds.FindAsync(model.RoundId);
            if (entity?.OwnerId != _userId)
                return false;

            entity.MatchDate = model.MatchDate;
            entity.MatchScore = model.MatchScore;

            return await _context.SaveChangesAsync() == 1;
            
        }
    }
}
