using PokeBattler.Common.Models.Enums;
using UnityEngine;

namespace PokeBattler.Common.Models
{
    public class PokemonMove : ScriptableObject
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
