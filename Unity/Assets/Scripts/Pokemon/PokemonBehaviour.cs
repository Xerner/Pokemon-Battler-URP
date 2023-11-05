using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class PokemonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    #region  Varables

    SpriteRenderer sprite;
    Animator animator;
    new BoxCollider2D collider;

    public PokemonStats Stats;

    [SerializeField][Pokemon] PokemonScriptableObject pokemonSO;
    [SerializeField] Pokemon pokemon;
    [SerializeField] public PokemonCombat Combat;
    //public bool isSelected;
    public SnapTo CurrentField;
    public Trainer trainer;
    public Action<PokemonBehaviour> OnDestroyed;
    [Header("Pokemon API")][SerializeField] string pokemonName;

    public Pokemon Pokemon { get { return pokemon; } }

    #endregion

    void Start() {
        InitializeComponents();
        if (pokemonSO != null) Initialize(pokemonSO.Pokemon);
    }

    /// <summary>Invokes the PokemonBehaviours OnDestroyed Action and Resets its current PokeContainer</summary>
    void OnDestroy() {
        //if (CurrentField == null) throw new System.Exception($"{id}\t{name} : CurrentField == null!");
        CurrentField?.Reset();
        OnDestroyed?.Invoke(this);
    }
    
    /// <summary>Initializes the SpriteRenderer and Animator components</summary>
    void InitializeComponents() {
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
        if (GetComponent<Collider>() == null) collider = GetComponent<BoxCollider2D>();
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
            transform.RectTransform().sizeDelta = pokemon.TrueSpriteSize;
            collider.size = pokemon.TrueSpriteSize;
            gameObject.name = pokemon.name;
            Stats = new PokemonStats(pokemon);
        } else {
            Debug.LogError("Null Pokemon object given");
        }
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

    public void OnPointerEnter(PointerEventData eventData) {
        PokemonHoverbox.Instance.Show(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (eventData.pointerCurrentRaycast.gameObject != PokemonHoverbox.Instance.gameObject)
            PokemonHoverbox.Instance.OnPointerExit();
    }

    [Serializable]
    public class PokemonStats {
        [SerializeField] int hp;
        [SerializeField] int pp;
        [SerializeField] int attack;
        [SerializeField] int defense;
        [SerializeField] int specialAttack;
        [SerializeField] int specialDefense;
        [SerializeField] float attackSpeed;

        public PokemonStats(Pokemon pokemon) {
            hp = pokemon.Hp.baseStat;
            pp = 100; //pokemon.Pp.baseStat;
            attack = pokemon.Attack.baseStat;
            defense = pokemon.Defense.baseStat;
            specialAttack = pokemon.SpecialAttack.baseStat;
            specialDefense = pokemon.SpecialDefense.baseStat;
            attackSpeed = pokemon.Speed.baseStat / 100f;
        }

        public int HP { get => hp; }
        public int PP { get => pp; }
        public int Attack { get => attack; }
        public int Defense { get => defense; }
        public int SpecialAttack { get => specialAttack; }
        public int SpecialDefense { get => specialDefense; }
        public float AttackSpeed { get => attackSpeed; }
    }
}
