using System.ComponentModel.DataAnnotations;

namespace TeamManagementService.Model
{
    public class Team
    {
        //[Key]
        //public int Id { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int NumberOfTeam {  get; set; }

    }
}
