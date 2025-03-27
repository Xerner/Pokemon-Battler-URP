using System;
using System.Collections.Generic;

namespace AutoChess.Contracts.DTOs
{
    public class UnitClaimedDTO : BaseDTO
    {
        public Guid AccountId { get; set; }
        public Guid UnitId { get; set; }
        public int NewMoneyBalance { get; set; }
        public Guid? UnitPromoted { get; set; } = null;
        public IEnumerable<Guid>? UnitsToDestroy { get; set; } = null;
        public MoveUnitDTO? Move { get; set; } = null;
    }
}
