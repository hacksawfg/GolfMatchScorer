using GolfMatchScore.Server.Services.RoundServices;
using GolfMatchScore.Shared.Models.GolfRound;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GolfRoundController : ControllerBase
    {
        private readonly IRoundService _roundService;

        public GolfRoundController(IRoundService roundService)
        {
            _roundService = roundService;
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
            _roundService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var golfRounds = await _roundService.ListAllRoundsAsync();
            return Ok(golfRounds.ToList());
        }

        [HttpGet("{roundId}")]
        public async Task<IActionResult> Round(int roundId)
        {
            var golfRound = await _roundService.GetRoundDetailsByIdAsync(roundId);
            if (golfRound == null)
                return NotFound();

            return Ok(golfRound);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoundCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!SetUserIdInService())
                return Unauthorized();

            bool roundCreated = await _roundService.CreateRoundAsync(model);
            if (roundCreated)
                return Ok();

            return UnprocessableEntity();
        }

        [HttpPut("edit/{roundId}")]
        public async Task<IActionResult> Edit(int roundId, RoundEdit model)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            if (model == null || !ModelState.IsValid)
                return BadRequest();

            bool editedRound = await _roundService.UpdateRoundById(model);
            if (editedRound)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("delete/{roundId}")]
        public async Task<IActionResult> Delete(int roundId)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            var selectRound = await _roundService.GetRoundDetailsByIdAsync(roundId);
            if (selectRound == null)
                return NotFound();

            bool deletedRound = await _roundService.DeleteRoundByIdAsync(roundId);
            if (deletedRound)
                return Ok();

            return BadRequest();
        }


    }
}
