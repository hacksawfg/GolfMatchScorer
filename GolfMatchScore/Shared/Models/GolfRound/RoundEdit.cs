using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.GolfRound
{
    public class RoundEdit
    {
        public int RoundId { get; set; }
        public DateTime MatchDate { get; set; }
        public int MatchScore { get; set; }

        // Foreign Keys
        public int CourseId { get; set; }
        public int PlayerId { get; set; }
    }
}
