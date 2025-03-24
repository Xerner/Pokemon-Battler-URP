using System;
using AutoChess.Contracts.Enums;

namespace AutoChess.Contracts.Models
{
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
