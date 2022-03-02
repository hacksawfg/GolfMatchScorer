using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfMatchScore.Shared.Models.Player
{
    public class PlayerCreate
    {
        [Required]
        public string PlayerFirstName { get; set; }
        [Required]
        public string PlayerLastName { get; set; }
    }
}
