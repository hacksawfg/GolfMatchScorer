using GolfMatchScore.Shared.Models.GolfRound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.Course
{
    public class CourseDetail
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCity { get; set; }
        public string CourseState { get; set; }
        public int CourseSlope { get; set; }
        public double CourseDifficultyRating { get; set; }
        public int CoursePar { get; set; }
        public int CourseLength { get; set; }
        //public int RoundId { get; set; }
        public virtual ICollection<RoundListItem> Rounds { get; set; }
        public ICollection<string> MatchDates { get; set; }
    }
}
