using System;
using System.Collections.Generic;
using AutoChess.Contracts.Models;

namespace AutoChess.Contracts.DTOs
{
    public class UnitClaimedDTO : BaseDTO
    {
        public Guid AccountId { get; set; }
        public Unit Unit { get; set; }
        public IEnumerable<Guid> UnitsToDestroy { get; set; }
        public RefreshShopDTO Shop { get; set; }
        public MoveUnitDTO Move { get; set; }
    }
}
