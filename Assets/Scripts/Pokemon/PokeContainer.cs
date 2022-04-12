using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PokeContainer : MonoBehaviour
{
    [HideInInspector]
    public PokemonBehaviour HeldPokemon;

    public abstract void Reset();
    public abstract void SetPokemon(PokemonBehaviour pokemon);
}
