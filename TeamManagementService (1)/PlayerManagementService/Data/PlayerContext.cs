/*using PlayerManagementService.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace PlayerManagementService.Models
{
    public class PlayerContext
    {
        [Key]
        public int PlayerId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
    }
}*/
using Microsoft.EntityFrameworkCore;
using PlayerManagementService.Models;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;

namespace PlayerManagementService.Data
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options) { }
        public DbSet<Models.PlayerContext> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.PlayerContext>().HasData(
                new Models.PlayerContext { PlayerId = 1, Name = "Player One", IsAvailable = true },
                new Models.PlayerContext { PlayerId = 2, Name = "Player Two", IsAvailable = true },
                new Models.PlayerContext { PlayerId = 3, Name = "Player Three", IsAvailable = true }
            );
        }
    }
}
