using GolfMatchScore.Server.Services.TeamServices;
using GolfMatchScore.Shared.Models.Team;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            if (userIdClaim == null)
                return null;

            return userIdClaim;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null)
                return false;
            _teamService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams.ToList());
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> Team(int teamId)
        {
            var team = await _teamService.GetTeamDetailByIdAsync(teamId);

            if (team == null)
                return NotFound();

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamCreate model)
        {
            if (!ModelState.IsValid || model is null)
                return BadRequest();

            if (!SetUserIdInService())
                return Unauthorized();

            bool teamCreated = await _teamService.CreateTeamAsync(model);
            if (teamCreated)
                return Ok();
            else
                return UnprocessableEntity();
        }

        [HttpPut("edit/{teamId}")]
        public async Task<IActionResult> Edit(int teamId, TeamEdit model)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            if (model == null || !ModelState.IsValid)
                return BadRequest();

            if (model.TeamId != teamId)
                return BadRequest();

            bool editedNote = await _teamService.UpdateTeamByIdAsync(model);

            if (editedNote)
                return Ok();
            return BadRequest();

        }

        [HttpDelete("delete/{teamId}")]
        public async Task<IActionResult> Delete(int teamId)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            var selectTeam = await _teamService.GetTeamDetailByIdAsync(teamId);
            if (selectTeam == null)
                return NotFound();

            bool deleteTeam = await _teamService.DeleteTeamByIdAsync(teamId);
            if (deleteTeam)
                return Ok();
            return BadRequest();
        }

    }
}
