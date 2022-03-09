using GolfMatchScore.Server.Data;
using GolfMatchScore.Server.Models;
using GolfMatchScore.Shared.Models.Player;
using GolfMatchScore.Shared.Models.Team;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Services.TeamServices
{
    public class TeamService : ITeamService
    {
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;

        private readonly ApplicationDbContext _context;
        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTeamAsync(TeamCreate model)
        {
            var teamEntity = new Team
            {
                OwnerId = _userId,
                TeamSchool = model.TeamSchool,
                TeamCoachFirstName = model.TeamCoachFirstName,
                TeamCoachLastName = model.TeamCoachLastName,
            };

            _context.Teams.Add(teamEntity);
            var numChanges = await _context.SaveChangesAsync();
            return numChanges == 1;
        }

        public async Task<bool> DeleteTeamByIdAsync(int teamId)
        {
            var entity = await _context.Teams.FindAsync(teamId);
            if (entity?.OwnerId != _userId)
                return false;

            _context.Teams.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
            
        }

        public async Task<IEnumerable<TeamListItem>> GetAllTeamsAsync()
        {
            var teamQuery = await _context.Teams.Select(t => new TeamListItem
            {
                TeamId = t.TeamId,
                TeamSchool = t.TeamSchool
            }).ToListAsync();

            return teamQuery;
        }

        public async Task<TeamDetail> GetTeamDetailByIdAsync(int teamId)
        {
            var teamQuery = await _context.Teams
                .Include(p => p.Players)
                .FirstOrDefaultAsync(entity => entity.TeamId == teamId);

            return teamQuery is null ? null : new TeamDetail
            {
                TeamId = teamId,
                TeamSchool = teamQuery.TeamSchool,
                TeamCoachFirstName = teamQuery.TeamCoachFirstName,
                TeamCoachLastName = teamQuery.TeamCoachLastName,
                Players = teamQuery.Players.Select(p => new PlayerListItem
                {
                    PlayerId = p.PlayerId,
                    PlayerFirstName = p.PlayerFirstName,
                    PlayerLastName = p.PlayerLastName
                }).ToList()
            };
        }


        public async Task<bool> UpdateTeamByIdAsync(TeamEdit model)
        {
            if (model == null)
                return false;

            var entity = await _context.Teams.FindAsync(model.TeamId);
            if (entity?.OwnerId != _userId)
                return false;

            entity.TeamSchool = model.TeamSchool;
            entity.TeamCoachFirstName = model.TeamCoachFirstName;
            entity.TeamCoachLastName = model.TeamCoachLastName;
            
            return await _context.SaveChangesAsync() == 1;
        }
    }
}
