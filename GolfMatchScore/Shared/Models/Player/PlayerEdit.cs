using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.Player
{
    public class PlayerEdit
    {
        public int PlayerId { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public int TeamId { get; set; }
    }
}
