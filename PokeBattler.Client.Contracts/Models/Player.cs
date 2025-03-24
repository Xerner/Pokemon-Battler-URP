using System;
using System.Collections.Generic;
using AutoChess.Contracts.Interfaces;

namespace AutoChess.Contracts.Models
{
    public class Player
    {
        public Guid AccountId { get; set; }
        public Guid GameId { get; set; }
        public bool Ready { get; set; } = false;
        public int CurrentHealth { get; set; } = 100;
        public int TotalHealth { get; set; } = 100;
        public int Experience { get; set; } = 0;
        public int Money { get; set; } = 10;
        public int Level { get; set; } = 1;
        public int ExperienceNeededToLevelUp { get; set; }
        public Game Game { get; set; }
        public IEnumerable<IAutoChessUnitContainer> UnitContainers { get; set; }
    }
}
