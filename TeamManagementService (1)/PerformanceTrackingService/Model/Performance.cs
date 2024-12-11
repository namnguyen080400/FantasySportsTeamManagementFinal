using System.ComponentModel.DataAnnotations;

namespace PerformanceTrackingService.Model
{
    public class Performance
    {
        [Key]
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int GamesPlayed { get; set; } = 0;
        public int TotalScore { get; set; } = 0;
        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;
    }
}