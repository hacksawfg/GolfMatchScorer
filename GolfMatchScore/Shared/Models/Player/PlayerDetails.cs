using GolfMatchScore.Shared.Models.GolfRound;
using GolfMatchScore.Shared.Models.Team;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.Player
{
    public class PlayerDetails
    {
        public int PlayerId { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public virtual TeamListItem Team { get; set; }
        public virtual ICollection<RoundListItem> Rounds { get; set; } = new List<RoundListItem>();
    }
}
