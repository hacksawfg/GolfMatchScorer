using GolfMatchScore.Shared.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.Team
{
    public class TeamDetail
    {
        public int TeamId { get; set; }
        public string TeamSchool { get; set; }
        public string TeamCoachFirstName { get; set; }
        public string TeamCoachLastName { get; set; }
        public int PlayerId { get; set; }
        public virtual ICollection<PlayerListItem> Players { get; set; } = new List<PlayerListItem>();
    }
}
