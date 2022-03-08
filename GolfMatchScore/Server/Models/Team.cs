using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GolfMatchScore.Server.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string OwnerId { get; set; }
        [Required]
        public string TeamSchool { get; set; }
        [Required]
        public string TeamCoachFirstName { get; set; }
        [Required]
        public string TeamCoachLastName { get; set; }
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    }
}
