using System;
using AutoChess.Contracts.Interfaces;

namespace AutoChess.Contracts.Models
{
    public class Unit
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        public Guid? AccountId { get; set; }
        public int CombinationStage { get; set; } = 1;
        public int Cost { get; set; } = 1;
        public int SellValue { get; set; } = 1;
        public bool IsCountedInPool { get; private set; } = true;
        public int CountTowardsPool { get; private set; } = 1;
        public IUnitContainer? Container { get; set; } = null;
        public UnitInfo Info { get; set; }

        public Unit() { }

        public Unit(bool IsCountedInPool, int CountTowardsPool)
        {
            this.IsCountedInPool = IsCountedInPool;
            this.CountTowardsPool = CountTowardsPool;
        }
    }
}
