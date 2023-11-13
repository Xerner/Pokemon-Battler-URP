using Poke.Core;
using UnityEngine.EventSystems;

namespace Poke.Unity
{
    public interface IPokeContainer : IPointerDownHandler
    {
        public PokemonBehaviour Pokemon { get; }
        /// <summary>Returns whether or not the Pokemon should still be stuck to the cursor</summary>
        public bool SetPokemon(PokemonBehaviour Pokemon);
    }
}
