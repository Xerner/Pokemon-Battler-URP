using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaContainer : PokeContainer
{
    public static readonly Vector2Int UnitVector = new Vector2Int(128, 128);

    public Vector2Int Index;
    public Allegiance Allegiance;
    public PokemonBehaviour CombatPokemon;

    public override void Reset()
    {
        
    }

    public override void SetPokemon(PokemonBehaviour pokemon)
    {
        throw new System.NotImplementedException();
    }
}
