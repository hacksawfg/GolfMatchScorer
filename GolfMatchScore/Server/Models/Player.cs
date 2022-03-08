using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfMatchScore.Server.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string PlayerFirstName { get; set; }
        [Required]
        public string PlayerLastName { get; set; }
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<GolfRound> Rounds { get; set; } = new List<GolfRound>();
    }
}
