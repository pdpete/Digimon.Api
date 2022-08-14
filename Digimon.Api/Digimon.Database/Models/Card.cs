using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon.Database.Models
{
    public class Card
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string SetId { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Level { get; set; }
        public int? DP { get; set; }
        public int? PlayCost { get; set; }
        [Required]
        public Rarity Rarity { get; set; }
        public CardType Type { get; set; }
        public List<Digivolution> Digivolutions { get; set; }
        public List<Color> Colors { get; set; }
        public List<Trait> Traits { get; set; }
    }
}
