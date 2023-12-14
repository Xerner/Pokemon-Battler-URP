using PokeBattler.Common.Models;
using PokeBattler.Common.Models.Enums;
using PokeBattler.Common.Models.Interfaces;
using UnityEngine;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Arena")]
    public class ArenaBehaviour : MonoBehaviour
    {
        public Arena Arena;

        public PokemonBehaviour AddPokemonToContainer(EContainerType containerType, int index, PokemonBehaviour pokemon)
        {
            IPokeContainer container = Arena.PokeContainers[containerType][index];
            pokemon.MoveTo.ShouldLerpToPosition = false;
            pokemon.SetPokeContainer(container);
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
