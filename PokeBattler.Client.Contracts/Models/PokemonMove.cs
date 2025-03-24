using AutoChess.Contracts.Enums;

namespace AutoChess.Contracts.Models
{
    public class PokemonMove
    {
        public string Name;
        public string Description;
        public EPokemonType Type = EPokemonType.Normal;
        public EDamageType DamageType = EDamageType.Physical;
        public int ppNeeded = 100;
        public int ppRecovered = 0;
        public int Range = 1;

        public bool IsRanged { get { return Range > 1; } }
    }
}
