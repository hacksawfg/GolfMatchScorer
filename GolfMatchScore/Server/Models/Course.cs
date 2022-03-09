using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GolfMatchScore.Server.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string CourseName { get; set; }
        public string CourseCity { get; set; }
        public string CourseState { get; set; }
        public int CourseSlope { get; set; }
        public double CourseDifficultyRating { get; set; }
        [Required]
        public int CoursePar { get; set; }
        public int CourseLength { get; set; }
        public virtual ICollection<GolfRound> Rounds { get; set; } = new List<GolfRound>();
    }
}
