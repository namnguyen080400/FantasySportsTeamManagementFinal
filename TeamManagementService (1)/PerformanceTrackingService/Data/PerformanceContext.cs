using Microsoft.EntityFrameworkCore;
using PerformanceTrackingService.Model;

namespace PerformanceTrackingService.Data
{
    public class PerformanceContext: DbContext
    {
        public PerformanceContext(DbContextOptions<PerformanceContext>options):base(options)
        {
        }
        public DbSet<Performance>Performances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Performance>().HasData(
                new Performance
                {
                    Id =1,
                    PlayerId=1,
                    GamesPlayed = 0,
                    TotalScore = 0,
                    Wins = 0,
                    Losses = 0,
    },
                 new Performance
                 {
                     Id = 2,
                     PlayerId = 2,
                     GamesPlayed = 0,
                     TotalScore = 0,
                     Wins = 0,
                     Losses = 0,
                 },
                  new Performance
                  {
                      Id = 3,
                      PlayerId = 3,
                      GamesPlayed = 0,
                      TotalScore = 0,
                      Wins = 0,
                      Losses = 0,
                  }

                );
        }

    }
}
