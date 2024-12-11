using System.ComponentModel.DataAnnotations;

namespace PlayerManagementService.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
    }
}
