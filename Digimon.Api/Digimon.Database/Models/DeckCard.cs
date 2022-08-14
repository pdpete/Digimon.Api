using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon.Database.Models
{
    public class DeckCard
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Card Card { get; set; }
        public Deck Deck { get; set; }
    }
}
