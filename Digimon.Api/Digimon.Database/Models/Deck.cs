using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon.Database.Models
{
    public class Deck
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public User User { get; set; }
    }
}
