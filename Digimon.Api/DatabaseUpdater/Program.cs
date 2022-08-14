using Digimon;
using Digimon.Database;
using Digimon.Database.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace DatabaseUpdater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Trace.AutoFlush = true;
            Trace.Listeners.Add(new ConsoleTraceListener());

            try
            {
                var ctx = new DigimonContext();
                var file = @"API Results/digimonFull.json";

                var digimonText = File.ReadAllText(file);
                var cards = JsonConvert.DeserializeObject<List<DigimonCard>>(digimonText) ?? new List<DigimonCard>();

                foreach (var card in cards)
                {
                    Trace.WriteLine($"Processing Card: {card.CardNumber}");
                    ProcessCard(card, ctx);
                    ctx.SaveChanges();
                }
            }
            catch (Exception? ex)
            {
                while (ex != null)
                {
                    Trace.WriteLine(ex.ToString());
                    ex = ex.InnerException;
                }
            }
        }

        private static Card ProcessCard(DigimonCard card, DigimonContext ctx)
        {
            var type = card.CardType switch
            {
                "Digimon" => 1,
                "Option" => 2,
                "Digi-Egg" => 3,
                "Tamer" => 4,
                _ => throw new Exception("Missing Type")
            };

            if (card.CardNumber.StartsWith("P") && string.IsNullOrEmpty(card.Rarity))
                card.Rarity = "P";

            var rarity = card.Rarity switch
            {
                "P" => 1,
                "C" => 2,
                "U" => 3,
                "R" => 4,
                "SR" => 5,
                "SEC" => 6,
                _ => throw new Exception("Missing Rarity")
            };

            var level = ParseLevelString(card.Lv);

            int? cost = null;
            if (int.TryParse(card.PlayCost, out var c))
                cost = c;

            var digimonCard = ctx.Cards.Where(x => x.SetId == card.CardNumber).FirstOrDefault();

            if (digimonCard == null)
            {
                digimonCard = new Card()
                {
                    Id = card.ID,
                    SetId = card.CardNumber,
                    DP = card.DP,
                    Level = level,
                    Name = card.CardName,
                    PlayCost = cost,
                    Rarity = ctx.Rarities.Where(x => x.Id == rarity).First(),
                    Type = ctx.CardTypes.Where(x => x.Id == type).First(),
                    Traits = ProcessTraits(card, ctx),
                    Colors = ProcessColors(card, ctx),
                    Digivolutions = ProcessDigivolutions(card, ctx)
                };

                ctx.Cards.Add(digimonCard);
            }
            else
            {
                digimonCard.DP = card.DP;
                digimonCard.Level = level;
                digimonCard.Name = card.CardName;
                digimonCard.PlayCost = cost;
                digimonCard.Rarity = ctx.Rarities.Where(x => x.Id == rarity).First();
                digimonCard.Type = ctx.CardTypes.Where(x => x.Id == type).First();
                digimonCard.Traits = ProcessTraits(card, ctx);
                digimonCard.Colors = ProcessColors(card, ctx);
                digimonCard.Digivolutions = ProcessDigivolutions(card, ctx);
            }

            return digimonCard;
        }

        private static List<Color> ProcessColors(DigimonCard card, DigimonContext ctx)
        {
            var colorIds = new List<long>();

            if (!string.IsNullOrWhiteSpace(card.Color))
                colorIds.Add(GetColorId(card.Color));

            if (!string.IsNullOrWhiteSpace(card.Color2))
                colorIds.Add(GetColorId(card.Color2));

            return ctx.Colors.Where(x => colorIds.Contains(x.Id)).ToList();
        }

        private static List<Digivolution> ProcessDigivolutions(DigimonCard card, DigimonContext ctx)
        {
            var retval = new List<Digivolution>();

            if (!string.IsNullOrWhiteSpace(card.DigivolveCost1EvolutionSourceColor))
                retval.Add(UpsertDigivolution(card.DigivolveCost1EvolutionSourceColor, card.DigivolveCost1, card.ID, ctx));

            if (!string.IsNullOrWhiteSpace(card.DigivolveCost2EvolutionSourceColor))
                retval.Add(UpsertDigivolution(card.DigivolveCost2EvolutionSourceColor, card.DigivolveCost2, card.ID, ctx));

            return retval;
        }

        private static Digivolution UpsertDigivolution(string sourceColor, string? costString, long cardId, DigimonContext ctx)
        {
            var costRegex = new Regex("^[0-9]+");

            int cost = 0;

            if (!string.IsNullOrWhiteSpace(costString))
            {
                var digivolveCost = costRegex.Match(costString);

                if (!int.TryParse(digivolveCost.Value, out cost))
                    cost = 0;
            }

            var digivolution = ctx.Digivolutions.Where(x => x.Card.Id == cardId && sourceColor == x.Color.Name).FirstOrDefault();

            if (digivolution == null)
            {
                digivolution = digivolution ?? new Digivolution()
                {
                    Color = ctx.Colors.Where(x => x.Id == GetColorId(sourceColor)).First(),
                    Cost = cost,
                };
            }
            else
            {
                digivolution.Cost = cost;
            }

            return digivolution;
        }

        private static List<Trait> ProcessTraits(DigimonCard card, DigimonContext ctx)
        {
            var retval = new List<Trait>();

            if (!string.IsNullOrWhiteSpace(card.Form))
                retval.Add(UpsertTrait(card.Form, ctx));

            if (!string.IsNullOrWhiteSpace(card.Attribute) && card.Attribute != card.Form)
                retval.Add(UpsertTrait(card.Attribute, ctx));

            if (!string.IsNullOrWhiteSpace(card.Type) && card.Type != card.Form && card.Type != card.Attribute)
                retval.Add(UpsertTrait(card.Type, ctx));

            return retval;
        }

        private static Trait UpsertTrait(string traitName, DigimonContext ctx)
        {
            var trait = ctx.Traits.Where(x => x.Name == traitName).FirstOrDefault();
            trait = trait ?? new Trait()
            {
                Name = traitName
            };

            return trait;
        }

        private static long GetColorId(string color)
        {
            return color switch
            {
                "Black" => 1,
                "Red" => 2,
                "Green" => 3,
                "Blue" => 4,
                "Yellow" => 5,
                "Purple" => 6,
                "White" => 7,
                "All Color" => 7,
                _ => throw new Exception("Missing Color")
            };
        }

        private static int? ParseLevelString(string? levelString)
        {
            var lvString = levelString?.Replace("lv", "", StringComparison.CurrentCultureIgnoreCase).Replace(".", "").Trim();

            int? level = null;
            if (int.TryParse(lvString, out var lv))
                level = lv;

            return level;
        }
    }
}