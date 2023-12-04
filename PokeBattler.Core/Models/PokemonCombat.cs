using System;
using UnityEngine;

namespace PokeBattler.Common.Models {
    [Serializable]
    public class PokemonCombat
    {
        public bool combatMode;
        public ArenaSpot combatField;
        public Allegiance Allegiance;
        [SerializeField] private Pokemon targetEnemy;
        //private Pathing Path;
        public bool invulnerable;
    }

}
