using Microsoft.EntityFrameworkCore;
using PlayerManagementService.Models;
using System.Numerics;

namespace PlayerManagementService.Data
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options) { }

        public DbSet<Players> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Players>().HasData(
                new Players { playerId = 1, Name = "Player One", IsAvailable = true },
                new Players { playerId = 2, Name = "Player Two", IsAvailable = true },
                new Players { playerId = 3, Name = "Player Three", IsAvailable = true }
            );
        }
    }
}
