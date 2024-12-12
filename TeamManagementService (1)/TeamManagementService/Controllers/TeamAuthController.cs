using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManagementService.Data;
using TeamManagementService.Model;

namespace TeamManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamAuthController : ControllerBase
    {
        private readonly string _secretKey;
        private readonly TeamContext _context;

        public TeamAuthController(IConfiguration configuration, TeamContext context)
        {
            _secretKey = configuration["Jwt:SecretKey"];
            _context = context;
        }

        // Create team
        [HttpPost("createTeam")]
        public async Task<IActionResult> CreateTeam([FromBody] Team team)
        {
            if (string.IsNullOrEmpty(team.TeamName))
            {
                return BadRequest("Team name is required");
            }

            // Check if team name already exists in database
            if (await _context.Teams.AnyAsync(x => x.TeamName == team.TeamName))
            {
                return BadRequest("Team already exists");
            }

            // Assign new team id (This will be handled by the database if using auto-increment)
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return Ok("Successfully created a team");
        }

        // Update team
        [HttpPost("UpdateTeam")]
        public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeam updateTeam)
        {
            var team = await _context.Teams.FindAsync(updateTeam.TeamId);
            if (team == null)
            {
                return NotFound("Cannot find the team id");
            }

            // Update team details
            team.NumberOfTeam = updateTeam.NumberOfTeam;
            await _context.SaveChangesAsync();

            return Ok("Team has been updated");
        }

        [HttpGet]
        public async Task<ActionResult<List<Team>>> GetAllTeams()
        {
            var teams = await _context.Teams.ToListAsync();
            return Ok(teams);
        }

    }
}
