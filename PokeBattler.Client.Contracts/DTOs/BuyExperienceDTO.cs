using System.Collections.Generic;

namespace AutoChess.Contracts.DTOs
{
    public class BuyExperienceDTO : BaseDTO
    {
        public int Money { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceNeededToLevelUp { get; set; }
        public IEnumerable<int> TierChances { get; set; } = [];
    }
}
