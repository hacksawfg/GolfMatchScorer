using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.Course
{
    public class CourseListItem
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCity { get; set; }
        public string CourseState { get; set; }
    }
}
