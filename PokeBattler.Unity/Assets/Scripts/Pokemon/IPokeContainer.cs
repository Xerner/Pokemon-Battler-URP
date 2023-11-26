using PokeBattler.Core;
using UnityEngine.EventSystems;

namespace PokeBattler.Unity
{
    public interface IPokeContainer : IPointerDownHandler
    {
        public PokemonBehaviour Pokemon { get; }
        /// <summary>Returns whether or not the Pokemon should still be stuck to the cursor</summary>
        public bool SetPokemon(PokemonBehaviour Pokemon);
    }
}
