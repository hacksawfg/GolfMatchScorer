using GolfMatchScore.Shared.Models.Player;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfMatchScore.Server.Models
{
    public class GolfRound
    {
        [Key]
        public int RoundId { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public DateTime MatchDate { get; set; }
        public int MatchScore { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(Player))]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

    }
}
