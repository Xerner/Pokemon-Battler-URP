using PokeBattler.Common.Models.Enums;
using PokeBattler.Common.Models.Interfaces;

namespace PokeBattler.Common.Models
{
    public class ArenaSpot : IPokeContainer
    {
        public static readonly Vector2Int UnitVector = new(128, 128);

        public Vector2Int Index = Vector2Int.Zero;
        public EAllegiance Allegiance;
        public Pokemon Pokemon { get; set; } = new();
    }
}
