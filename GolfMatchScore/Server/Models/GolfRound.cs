using System;
using System.ComponentModel.DataAnnotations;

namespace GolfMatchScore.Server.Models
{
    public class GolfRound
    {
        [Key]
        public int RoundId { get; set; }
        [Required]
        public DateTime MatchDate { get; set; }
        public int MatchScore { get; set; }

        // Foreign Keys
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
