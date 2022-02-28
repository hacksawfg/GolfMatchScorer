using System.ComponentModel.DataAnnotations;

namespace GolfMatchScore.Server.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        [Required]
        public string PlayerFirstName { get; set; }
        [Required]
        public string PlayerLastName { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
