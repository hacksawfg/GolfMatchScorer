using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.Team
{
    public class TeamCreate
    {
        [Required]
        public string TeamSchool { get; set; }
        [Required]
        public string TeamCoachFirstName { get; set; }
        [Required]
        public string TeamCoachLastName { get; set; }
    }
}
