using PokeBattler.Common;
using PokeBattler.Common.Models;
using UnityEngine;

namespace PokeBattler.Unity {
    public partial class PokemonBehaviour : MonoBehaviour
    {
        /// <summary>Spawns a PokemonGO prefab and instantiates its PokemonBehaviour</summary>
        //public static PokemonBehaviour Spawn(string pokemonName) => Spawn(Common.Models.Pokemon.CachedPokemon[pokemonName]);

        /// <summary>Spawns a PokemonGO prefab and instantiates its PokemonBehaviour</summary>
        public static PokemonBehaviour Spawn(Pokemon pokemon)
        {
            PokemonBehaviour pokemonBehaviour = Instantiate(StaticAssets.Prefabs["PokemonGO"]).GetComponent<PokemonBehaviour>();
            pokemonBehaviour.Initialize(pokemon);
            pokemonBehaviour.MoveTo = pokemonBehaviour.GetComponent<MoveToBehaviour>();
            return pokemonBehaviour;
        }

        /// <summary>Spawns a PokemonGO prefab and instantiates its PokemonBehaviour</summary>
        //public static PokemonBehaviour Spawn(string pokemonName, Transform moveToTransform) => Spawn(Common.Models.Pokemon.CachedPokemon[pokemonName], moveToTransform);

        /// <summary>Spawns a PokemonGO prefab and instantiates its PokemonBehaviour</summary>
        public static PokemonBehaviour Spawn(Pokemon pokemon, Transform moveToTransform)
        {
            var pokemonBehaviour = Spawn(pokemon);
            pokemonBehaviour.MoveTo.MoveTo(moveToTransform);
            return pokemonBehaviour;
        }
    }
}

