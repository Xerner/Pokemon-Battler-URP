using AutoChess.Contracts.Enums;
using System;

namespace AutoChess.Contracts.Interfaces
{
    public interface IAutoChessUnitContainer
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? AccountId { get; set; }
        public EContainerType Tags { get; set; }
    }
}
