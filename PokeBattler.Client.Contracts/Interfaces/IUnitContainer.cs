using AutoChess.Contracts.Enums;
using System;

namespace AutoChess.Contracts.Interfaces
{
    public interface IUnitContainer
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? AccountId { get; set; }
        public EContainerTag Tags { get; set; }
    }
}
