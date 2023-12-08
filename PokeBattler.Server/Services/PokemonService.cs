using PokeBattler.Common.Models.Enums;
using PokeBattler.Common.Models.Json;
using PokeBattler.Common.Models;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace PokeBattler.Server.Services
{
    public class PokemonService
    {

        /// <summary>Initializes the Pokemons class, name, sprite, and ScriptableObject</summary>
        public async Task Initialize() => await Initialize(pokemonName);

        /// <summary>Initializes the Pokemons class, name, sprite, and ScriptableObject. Called by FetchPokemonButton</summary>
        public async Task Initialize(string pokemonName)
        {
            if (pokemonName.Trim() != "")
            {
                var pokemon = await Pokemon.GetPokemonFromAPI(pokemonName);
                Initialize(pokemon);
            }
            else
            {
                Debug.LogError("Invalid pokemon Id or name given: " + pokemonName);
            }
        }

        /// <summary>Initializes the Pokemons class, name, sprite, and ScriptableObject</summary>
        public void Initialize(Pokemon pokemon)
        {
            Debug2.Log("Initializing PokemonBehaviour for " + pokemon.name, LogLevel.Detailed, gameObject);
            if (pokemon == null)
            {
                Debug2.LogError("Null PokemonGO object given", gameObject);
            }

            InitializeComponents();
            this.pokemon = pokemon;
            if (pokemonSO == null) pokemonSO = ScriptableObject.CreateInstance<PokemonSO>();

            if (pokemon != null)
            {
                pokemonSO.Pokemon = pokemon;
                sprite.sprite = pokemon.Sprite;
                collider.size = pokemon.TrueSpriteSize;
                gameObject.name = pokemon.name;
                transform.RectTransform().sizeDelta = pokemon.TrueSpriteSize;
            }

            Stats = new PokemonStats(pokemon);
        }

        /// <summary>If possible, evolves the PokemonGO to the next stage</summary>
        /// <returns>The next stage of evolution for the current PokemonGO</returns>
        public async Task Evolve()
        {
            await Initialize(pokemon.Evolutions[pokemon.EvolutionStage]);
        }
    }
}
