using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.Course
{
    public class CourseCreate
    {
        [Required]
        public string CourseName { get; set; }
        public string CourseCity { get; set; }
        public string CourseState { get; set; }
        public int CourseSlope { get; set; }
        public double CourseDifficultyRating { get; set; }
        [Required]
        public int CoursePar { get; set; }
        public int CourseLength { get; set; }
    }
}
