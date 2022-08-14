using Digimon.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon.Database
{
    public class DigimonContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("Host=localhost;Database=DigimonCards;Username=postgres;Password=aw3crtb")
                .UseSnakeCaseNamingConvention();
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<DeckCard> DeckCards { get; set; }
        public DbSet<Digivolution> Digivolutions { get; set; }
        public DbSet<Trait> Traits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCard> UserCards { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Rarity> Rarities { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
    }
}
