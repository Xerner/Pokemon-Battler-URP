using AutoChess.Contracts.Enums;
using AutoChess.Contracts.Interfaces;

namespace AutoChess.Contracts.Models
{
    public class ArenaSpot : IAutoChessUnitContainer
    {
        public static readonly Vector2Int UnitVector = new(128, 128);

        public Vector2Int Index = Vector2Int.Zero;
        public EAllegiance Allegiance;
        public Pokemon Pokemon { get; set; } = new();
    }
}
