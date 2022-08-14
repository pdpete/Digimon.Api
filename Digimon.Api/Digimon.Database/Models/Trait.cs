using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon.Database.Models
{
    public class Trait
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
    }
}
