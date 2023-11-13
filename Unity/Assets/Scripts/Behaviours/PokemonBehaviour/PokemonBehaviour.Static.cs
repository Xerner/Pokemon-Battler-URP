using Poke.Core;
using UnityEngine;

namespace Poke.Unity {
    public partial class PokemonBehaviour : MonoBehaviour
    {
        /// <summary>Spawns a Pokemon prefab and instantiates its PokemonBehaviour</summary>
        public static PokemonBehaviour Spawn(string pokemonName) => Spawn(Pokemon.CachedPokemon[pokemonName]);

        /// <summary>Spawns a Pokemon prefab and instantiates its PokemonBehaviour</summary>
        public static PokemonBehaviour Spawn(Pokemon pokemon)
        {
            PokemonBehaviour pokemonBehaviour = Instantiate(StaticAssets.Prefabs["Pokemon"]).GetComponent<PokemonBehaviour>();
            pokemonBehaviour.Initialize(pokemon);
            pokemonBehaviour.MoveTo = pokemonBehaviour.GetComponent<MoveToBehaviour>();
            return pokemonBehaviour;
        }

        /// <summary>Spawns a Pokemon prefab and instantiates its PokemonBehaviour</summary>
        public static PokemonBehaviour Spawn(string pokemonName, Transform moveToTransform) => Spawn(Pokemon.CachedPokemon[pokemonName], moveToTransform);

        /// <summary>Spawns a Pokemon prefab and instantiates its PokemonBehaviour</summary>
        public static PokemonBehaviour Spawn(Pokemon pokemon, Transform moveToTransform)
        {
            var pokemonBehaviour = Spawn(pokemon);
            pokemonBehaviour.MoveTo.MoveTo(moveToTransform);
            return pokemonBehaviour;
        }
    }
}

