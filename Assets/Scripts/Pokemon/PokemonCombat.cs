using System;
using UnityEngine;

[Serializable]
public class PokemonCombat {
    public bool combatMode;
    public ArenaCard combatField;
    public Allegiance Allegiance;
    [SerializeField] private PokemonBehaviour targetEnemy;
    //private Pathing Path;
    public bool invulnerable;
}
