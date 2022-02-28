using System.ComponentModel.DataAnnotations;

namespace GolfMatchScore.Server.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        [Required]
        public string TeamSchool { get; set; }
        [Required]
        public string TeamCoachFirstName { get; set; }
        [Required]
        public string TeamCoachLastName { get; set; }

    }
}
