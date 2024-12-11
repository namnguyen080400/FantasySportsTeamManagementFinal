using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceTrackingService.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] Login login)
    {
        // Hardcoded user validation for demo purposes
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
}

