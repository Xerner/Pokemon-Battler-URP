using System;
using System.Collections.Generic;
using AutoChess.Contracts.Options;

namespace AutoChess.Contracts.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public IGameOptions GameOptions { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<UnitInfo> UnitInfos { get; set; }
        public ICollection<UnitCount> UnitCounts { get; set; }
    }
}