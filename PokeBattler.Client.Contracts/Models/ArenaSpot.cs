using AutoChess.Contracts.Enums;
using AutoChess.Contracts.Interfaces;
using System;

namespace AutoChess.Contracts.Models
{
    public class ArenaSpot : IUnitContainer
    {
        public static readonly Vector2Int UnitVector = new(128, 128);

        public Vector2Int Index = Vector2Int.Zero;
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? AccountId { get; set; }
        public EContainerTag Tags { get; set; }
    }
}
