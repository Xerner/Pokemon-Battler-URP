
using System;
using System.Collections.Generic;

namespace AutoChess.Contracts.Models
{
    public class UnitInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Tier { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
