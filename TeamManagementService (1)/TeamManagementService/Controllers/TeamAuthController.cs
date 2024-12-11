using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TeamManagementService.Data;
using TeamManagementService.Model;

namespace TeamManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamAuthController : ControllerBase
    {
        private readonly string _secretKey;
        private readonly TeamContext context;
        private static readonly List<Team> teams = new List<Team>();
        private static readonly List<UpdateTeam> updateTeams = new List<UpdateTeam>();
        private static int CurrentTeamId = 0;

        public TeamAuthController(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"];
            this.context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            // Hardcoded user can do validation for demo purposes
            if (login.Username == "testuser" && login.Password == "password")
            {
                // Create token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("ThisIsAVeryLongSecretKeyThatIsAtLeast32BytesLong123");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, login.Username)
                }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }

            return Unauthorized("Invalid credentials");
        }

        // create team
        [HttpPost("createTeam")]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            // check to make sure user required to enter team name
            if (string.IsNullOrEmpty(team.TeamName))
            {
                return BadRequest("Team name is required");
            }
            // check if team name already taking
            if (teams.Any(x => x.TeamName == team.TeamName))
            {
                return BadRequest("Team already exist");
            }
            else
                team.TeamId = CurrentTeamId;
            CurrentTeamId++;
            teams.Add(team);
            return Ok("Sucessfully create a team");
        }

        // update team
        [HttpPost("UpdateTeam")]
        public IActionResult UpdateTeam([FromBody] UpdateTeam updateTeam)
        {
            // check if team id match
            var team = teams.Find(x => x.TeamId == updateTeam.TeamId);
            if (team == null) // team id does not match
            {
                return Ok("Cannot find the team id");
            }
            else // team id match
            {
                team.NumberOfTeam = updateTeam.NumberOfTeam;
                return Ok("Team has been updated");
            }
        }

        // retrive team based on team id
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> Get(int id)
        {
            var team = await context.Teams.FindAsync(id);
            //validation to see if the team exist
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }
    }
}
