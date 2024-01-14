using System;
using PokeBattler.Common.Models.Enums;

namespace PokeBattler.Common.Models {
    [Serializable]
    public class PokemonCombat
    {
        public bool combatMode;
        public ArenaSpot combatField;
        public EAllegiance Allegiance;
        private Pokemon targetEnemy;
        //private Pathing Path;
        public bool invulnerable;
    }

}
