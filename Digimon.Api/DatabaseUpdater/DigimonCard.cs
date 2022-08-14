using Newtonsoft.Json;

namespace Digimon
{
    public class DigimonCard
    {
        public long ID { get; set; }
        [JsonProperty(PropertyName = "Card Name")]
        public string CardName {get; set;}
        [JsonProperty(PropertyName = "Card Number")]
        public string CardNumber { get; set; }
        public string? Rarity { get; set; }
        [JsonProperty(PropertyName = "Card type")]
        public string? CardType {get; set;}
        [JsonProperty(PropertyName = "Lv.")]
        public string? Lv { get; set; }
        public string Color {get; set;}
        public string Color2 { get; set; }
        public string? Form {get; set;}
        public string? Attribute {get; set;}
        public string? Type { get; set; }
        public int? DP { get; set; }
        [JsonProperty(PropertyName = "Play Cost")]
        public string? PlayCost {get; set;}
        [JsonProperty(PropertyName = "Digivolve Cost 1 Evolution source Lv.")]
        public string? DigivolveCost1EvolutionSourceLv { get; set; }
        [JsonProperty(PropertyName = "Digivolve Cost 1 Evolution source Color")]
        public string? DigivolveCost1EvolutionSourceColor { get; set; }
        [JsonProperty(PropertyName = "Digivolve Cost 1")]
        public string? DigivolveCost1 { get; set; }
        [JsonProperty(PropertyName = "Digivolve Cost 2 Evolution Source Lv.")]
        public string? DigivolveCost2EvolutionSourceLv { get; set; }
        [JsonProperty(PropertyName = "Digivolve Cost 2 Evolution Source Color")]
        public string? DigivolveCost2EvolutionSourceColor { get; set; }
        [JsonProperty(PropertyName = "Digivolve Cost 2")]
        public string? DigivolveCost2 { get; set; }
    }
}