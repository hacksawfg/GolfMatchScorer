using GolfMatchScore.Shared.Models.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int PlayerId { get; set; }
        public virtual PlayerListItem Player { get; set; } 
    }
}
