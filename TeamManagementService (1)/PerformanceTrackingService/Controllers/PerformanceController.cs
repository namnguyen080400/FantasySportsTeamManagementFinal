using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceTrackingService.Data;
using PerformanceTrackingService.Model;

namespace PerformanceTrackingService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly PerformanceContext context;
        public PerformanceController(PerformanceContext performanceContext)
        {
            context = performanceContext;
        }
        [HttpPost("play")]
        public async Task<IActionResult> Play(int playerId)
        {
            var perform = await context.Performances.FirstOrDefaultAsync(p => p.PlayerId == playerId);
            int scorePoints =new Random().Next(0, 100);
            if (scorePoints > 51)
            {
                perform.Wins++;
            }
            else
            {
                perform.Losses++;
            }

            perform.TotalScore=perform.Wins-perform.Losses;
            perform.GamesPlayed++;
            context.SaveChanges();
            return Ok(perform);
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await context.Performances.ToListAsync());
        }
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus(int playerId) 
        {
            var perform = await context.Performances.FirstOrDefaultAsync(p => p.PlayerId == playerId);
            if (perform == null)
                return NotFound();
            return Ok(perform);
        }
    }
}
