using System;

namespace AutoChess.Contracts.DTOs
{
    public class PlayerLevelUpDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
    }
}
