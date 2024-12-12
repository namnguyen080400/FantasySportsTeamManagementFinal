using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerManagementService.Data;
using PlayerManagementService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerContext _context;

        public PlayerController(PlayerContext context)
        {
            _context = context;
        }


        // Get all players (drafted and available)
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Players>>> GetAllPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // Get player by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Players>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return player;
        }

        // Get only available (not drafted) players
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Players>>> GetAvailablePlayers()
        {
            return await _context.Players
                .Where(p => p.IsAvailable)
                .ToListAsync();
        }

        // Register a new player
        [HttpPost("register")]
        public async Task<IActionResult> RegisterPlayer([FromBody] Players newPlayer)
        {
            if (newPlayer == null || string.IsNullOrWhiteSpace(newPlayer.Name))
            {
                return BadRequest(new { message = "Invalid player data." });
            }

            newPlayer.IsAvailable = true; // Ensure the player is available when registered
            _context.Players.Add(newPlayer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Player registered successfully", player = newPlayer });
        }

        // Draft a player
        [HttpPost("{id}/draft")]
        public async Task<IActionResult> DraftPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            if (!player.IsAvailable)
            {
                return BadRequest(new { message = "Player is not available" });
            }

            player.IsAvailable = false;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Player drafted successfully" });
        }

        // Release a player
        [HttpPost("{id}/release")]
        public async Task<IActionResult> ReleasePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            player.IsAvailable = true;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Player released successfully" });
        }
    }
}
