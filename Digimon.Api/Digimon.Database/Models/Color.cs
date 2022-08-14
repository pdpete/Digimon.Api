using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon.Database.Models
{
    public class Color
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
    }
}
