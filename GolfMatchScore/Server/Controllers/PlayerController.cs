using GolfMatchScore.Server.Services.PlayerServices;
using GolfMatchScore.Shared.Models.Player;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
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
            _playerService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var players = await _playerService.ListAllPlayersAsync();
            return Ok(players.ToList());
        }

        [HttpGet("index/{playerId}")]
        public async Task<IActionResult> Player(int playerId)
        {
            var player = await _playerService.GetPlayerDetailsByIdAsync(playerId);
            if (player == null)
                return NotFound();

            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlayerCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            if (!SetUserIdInService())
                return Unauthorized();

            bool playerCreated = await _playerService.CreatePlayerAsync(model);
            if (playerCreated)
                return Ok();

            return BadRequest();
        }

        [HttpPut("edit/{playerId}")]
        public async Task<IActionResult> Edit(int playerId, PlayerEdit model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!SetUserIdInService())
                return Unauthorized();

            bool playerEdited = await _playerService.UpdatePlayerDetailsAsync(model);
            if (playerEdited)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("delete/{playerId}")]
        public async Task<IActionResult> Delete(int playerId)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            var selectPlayer = await _playerService.GetPlayerDetailsByIdAsync(playerId);
            if (selectPlayer == null)
                return NotFound();

            bool deletedPlayer = await _playerService.DeletePlayerByIdAsync(playerId);
            if (deletedPlayer)
                return Ok();

            return BadRequest();
        }
    }
}
