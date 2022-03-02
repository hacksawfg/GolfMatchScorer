using GolfMatchScore.Shared.Models.Course;
using GolfMatchScore.Shared.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.GolfRound
{
    public class RoundDetails
    {
        public int RoundId { get; set; }
        public DateTime MatchDate { get; set; }
        public int MatchScore { get; set; }

        // Foreign Keys
        public int CourseId { get; set; }
        public CourseListItem Course { get; set; }
        public int PlayerId { get; set; }
        public PlayerListItem Player { get; set; }
    }
}
