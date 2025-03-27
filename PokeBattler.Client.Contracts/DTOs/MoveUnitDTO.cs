using System;
using AutoChess.Contracts.Enums;

namespace AutoChess.Contracts.DTOs
{
    public class MoveUnitDTO : BaseDTO
    {
        public Guid AccountId { get; set; }
        public Guid UnitId { get; set; }
        public Guid ContainerId { get; set; }
    }
}
