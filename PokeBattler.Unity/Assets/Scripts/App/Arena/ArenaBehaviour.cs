using PokeBattler.Common.Models;
using PokeBattler.Common.Models.Interfaces;
using UnityEngine;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Arena")]
    public class ArenaBehaviour : MonoBehaviour
    {
        public Arena Arena;

        public PokemonBehaviour AddPokemonToBench(int index, PokemonBehaviour pokemon)
        {
            IPokeContainer bench = Arena.Bench[index];
            pokemon.MoveTo.ShouldLerpToPosition = false;
            pokemon.SetPokeContainer(bench);
            pokemon.MoveTo.ShouldLerpToPosition = true;
            return pokemon;
        }
    }

    public enum Allegiance
    {
        Ally,
        Enemy
    }
}
