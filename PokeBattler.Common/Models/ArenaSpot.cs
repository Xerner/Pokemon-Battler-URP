using PokeBattler.Common.Models.Enums;
using PokeBattler.Common.Models.Interfaces;
using UnityEngine;

namespace PokeBattler.Common.Models
{
    public class ArenaSpot : IPokeContainer
    {
        public static readonly Vector2Int UnitVector = new Vector2Int(128, 128);

        public Vector2Int Index;
        public EAllegiance Allegiance;
        public Pokemon Pokemon { get; set; }
    }
}
