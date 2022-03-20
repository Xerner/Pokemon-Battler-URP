using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCard : PokeContainer
{
    public static readonly Vector2Int UnitVector = new Vector2Int(128, 128);

    public Vector2Int Index;
    public Allegiance Allegiance;
    public Pokemon CombatPokemon;

    public override void Reset()
    {
        
    }

    public override void SetPokemon(Pokemon pokemon)
    {
        throw new System.NotImplementedException();
    }
}
