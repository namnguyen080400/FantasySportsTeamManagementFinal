using Microsoft.EntityFrameworkCore;
using PlayerManagementService.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PlayerManagementService.Data
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options) { }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasData(
                new Player { PlayerId = 1, Name = "Player One", IsAvailable = true },
                new Player { PlayerId = 2, Name = "Player Two", IsAvailable = true },
                new Player { PlayerId = 3, Name = "Player Three", IsAvailable = true }
            );
        }
    }
}
