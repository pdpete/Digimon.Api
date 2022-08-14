using Digimon.Database.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon.Database.Models
{
    public class Digivolution
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public Color Color { get; set; }
        [Required]
        public int Cost { get; set; }
        public Card Card { get; set; }
    }
}
