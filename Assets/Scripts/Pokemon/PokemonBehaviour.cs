using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PokemonBehaviour : MonoBehaviour {
    private SpriteRenderer sprite;
    private Animator animator;

    // TODO: create an Attribute to print the values of pokemon as uneditable labels
    [SerializeField]
    [Pokemon]
    private PokemonScriptableObject pokemonSO;
    [SerializeField]
    private Pokemon pokemon;
    [SerializeField]
    public PokemonCombat Combat;
    //public bool isSelected;
    public PokeContainer CurrentField;
    public Trainer trainer;

    public Action<PokemonBehaviour> OnDestroyed;

    [Header("Pokemon API")]
    [SerializeField]
    private string pokemonName;

    public Pokemon Pokemon { get; private set; }

    private void Start() {
        InitializeComponents();
    }

    /// <summary>Initializes the SpriteRenderer and Animator components</summary>
    private void InitializeComponents() {
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    /// <summary>Spawns a Pokemon prefab and instantiates its PokemonBehaviour</summary>
    public static PokemonBehaviour Spawn(string pokemonName) => Spawn(Pokemon.CachedPokemon[pokemonName]);

    /// <summary>Spawns a Pokemon prefab and instantiates its PokemonBehaviour</summary>
    public static PokemonBehaviour Spawn(Pokemon pokemon) {
        PokemonBehaviour pokemonBehaviour = Instantiate(StaticAssets.Prefabs["Pokemon"]).GetComponent<PokemonBehaviour>();
        pokemonBehaviour.Initialize(pokemon);
        return pokemonBehaviour;
    }

    /// <summary>Initializes the Pokemons class, name, sprite, and ScriptableObject</summary>
    public void Initialize() => Initialize(pokemonName);

    /// <summary>Initializes the Pokemons class, name, sprite, and ScriptableObject. Called by FetchPokemonButton</summary>
    public void Initialize(string pokemonName) {
        if (pokemonName.Trim() != "")
            Pokemon.GetPokemonFromAPI(pokemonName, (pokemon) => Initialize(pokemon));
        else
            Debug.LogError("Invalid pokemon ID or name given: " + pokemonName);
    }

    /// <summary>Initializes the Pokemons class, name, sprite, and ScriptableObject</summary>
    public void Initialize(Pokemon pokemon) {
        if (pokemon != null) {
            InitializeComponents();
            this.pokemon = pokemon;
            pokemonSO = ScriptableObject.CreateInstance<PokemonScriptableObject>();
            pokemonSO.Pokemon = pokemon;
            sprite.sprite = pokemon.Sprite;
            gameObject.name = pokemon.name;
        } else {
            Debug.LogError("Null Pokemon object given");
        }
    }

    /// <summary>Invokes the PokemonBehaviours OnDestroyed Action and Resets its current PokeContainer</summary>
    private void OnDestroy() {
        //if (CurrentField == null) throw new System.Exception($"{id}\t{name} : CurrentField == null!");
        CurrentField?.Reset();
        OnDestroyed?.Invoke(this);
    }

    /// <summary>If possible, evolves the Pokemon to the next stage</summary>
    /// <returns>The next stage of evolution for the current Pokemon</returns>
    public PokemonBehaviour Evolve() {
        // TODO: FIX
        ////PokemonBehaviour evolution = Instantiate(pokemon.evolution, transform).GetComponent<PokemonBehaviour>();
        //evolution.id = id;
        ////Network.IDtoPokemon[id] = evolution;
        //Destroy(this);
        //CurrentField.SetPokemon(evolution);
        //return evolution;
        return null;
    }
}
