using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.Team
{
    public class TeamEdit
    {
        public int TeamId { get; set; }
        public string TeamSchool { get; set; }
        public string TeamCoachFirstName { get; set; }
        public string TeamCoachLastName { get; set; }
    }
}
