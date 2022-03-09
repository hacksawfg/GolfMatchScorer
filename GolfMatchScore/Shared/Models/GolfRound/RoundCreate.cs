using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.GolfRound
{
    public class RoundCreate
    {
        [Required]
        public DateTime MatchDate { get; set; } = DateTime.Now;
        public int MatchScore { get; set; }

        // Foreign Keys
        public int CourseId { get; set; }
        public int PlayerId { get; set; }
    }
}
