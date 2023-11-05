using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPosition : MonoBehaviour
{
    public static readonly Vector2Int UnitVector = new Vector2Int(128, 128);

    public Vector2Int Index;
    public Allegiance Allegiance;
    public PokemonBehaviour CombatPokemon;
    public SnapTo snapTo;

    public PokemonBehaviour Pokemon {
        get {
            if (snapTo.SavedObject == null) return null;
            return snapTo.SavedObject?.GetComponent<PokemonBehaviour>();
        }
    }

    public void SetPokemon(PokemonBehaviour pokemon)
    {
        snapTo.SavedObject = pokemon.gameObject;
        snapTo.Snap();
    }
}
