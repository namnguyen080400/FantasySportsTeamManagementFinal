using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerManagementService.Data;
using PlayerManagementService.Models;
using System.Numerics;

namespace PlayerManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly Data.PlayerContext _context;

        public PlayerController(Data.PlayerContext context)
        {
            _context = context;
        }

        // Get all players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.PlayerContext>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // Get player by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.PlayerContext>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return player;
        }

        // Check player availability
        [HttpGet("{id}/availability")]
        public async Task<IActionResult> CheckAvailability(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player.IsAvailable);
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
                return BadRequest("Player is not available");
            }

            player.IsAvailable = false;
            await _context.SaveChangesAsync();
            return Ok("Player drafted successfully");
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
            return Ok("Player released successfully");
        }
    }
}
