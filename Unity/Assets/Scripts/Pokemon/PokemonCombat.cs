using Poke.Unity;
using System;
using UnityEngine;

namespace Poke.Core {
    [Serializable]
    public class PokemonCombat
    {
        public bool combatMode;
        public ArenaSpotBehaviour combatField;
        public Allegiance Allegiance;
        [SerializeField] private PokemonBehaviour targetEnemy;
        //private Pathing Path;
        public bool invulnerable;
    }

}
