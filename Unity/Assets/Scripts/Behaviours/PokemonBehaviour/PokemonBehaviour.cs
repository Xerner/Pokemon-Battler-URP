using Poke.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Poke.Unity {
    [AddComponentMenu("Poke Battler/Pokemon")]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(LookAtCameraBehaviour))]
    [RequireComponent(typeof(MoveToBehaviour))]
    public partial class PokemonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region  Varables

        SpriteRenderer sprite;
        Animator animator;
        new BoxCollider2D collider;

        public PokemonStats Stats;

        [SerializeField][Pokemon] PokemonSO pokemonSO;
        public Pokemon pokemon;
        [SerializeField] public PokemonCombat Combat;
        //public bool isSelected;
        public MoveToBehaviour MoveTo;
        public Trainer trainer;
        public Action<PokemonBehaviour> OnDestroyed;
        [Header("Pokemon API")][SerializeField] string pokemonName;

        public Pokemon Pokemon { get { return pokemon; } }

        #endregion

        #region Unity Events

        void Start()
        {
            InitializeComponents();
            Initialize(pokemonSO.Pokemon);
        }

        /// <summary>Invokes the PokemonBehaviours OnDestroyed Action and Resets its current PokeContainer</summary>
        void OnDestroy()
        {
            //if (MoveTo == null) throw new System.Exception($"{id}\t{name} : MoveTo == null!");
            MoveTo.MoveToOrigin(true);
            OnDestroyed?.Invoke(this);
        }

        #endregion

        #region Initialization

        /// <summary>Initializes the SpriteRenderer and Animator components</summary>
        void InitializeComponents()
        {
            if (sprite == null) sprite = GetComponent<SpriteRenderer>();
            if (animator == null) animator = GetComponent<Animator>();
            if (GetComponent<Collider>() == null) collider = GetComponent<BoxCollider2D>();
        }

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
                Debug.LogError("Invalid pokemon ID or name given: " + pokemonName);
            }
        }

        /// <summary>Initializes the Pokemons class, name, sprite, and ScriptableObject</summary>
        public void Initialize(Pokemon pokemon)
        {
            Debug2.Log("Initializing PokemonBehaviour for " + pokemon.name, LogLevel.Detailed, gameObject);
            if (pokemon == null)
            {
                Debug2.LogError("Null Pokemon object given", gameObject);
            }

            InitializeComponents();
            this.pokemon = pokemon;
            pokemonSO = ScriptableObject.CreateInstance<PokemonSO>();
            
            if (pokemon != null)
            {
                // GameObject settings
                pokemonSO.Pokemon = pokemon;
                sprite.sprite = pokemon.Sprite;
                collider.size = pokemon.TrueSpriteSize;
                gameObject.name = pokemon.name;
                transform.RectTransform().sizeDelta = pokemon.TrueSpriteSize;
            }

            Stats = new PokemonStats(pokemon);
        }

        #endregion

        /// <summary>If possible, evolves the Pokemon to the next stage</summary>
        /// <returns>The next stage of evolution for the current Pokemon</returns>
        public PokemonBehaviour Evolve()
        {
            // TODO: FIX
            ////PokemonBehaviour evolution = Instantiate(pokemon.evolution, transform).GetComponent<PokemonBehaviour>();
            //evolution.id = id;
            ////Network.IDtoPokemon[id] = evolution;
            //Destroy(this);
            //MoveTo.SetPokemon(evolution);
            //return evolution;
            return null;
        }

        #region Pointer Events

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (MoveToBehaviour.InstanceOnCursor == gameObject) 
                return;
            PokemonHoverboxBehaviour.Instance.Show(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject != PokemonHoverboxBehaviour.Instance.gameObject)
                PokemonHoverboxBehaviour.Instance.OnPointerExit();
        }

        #endregion

        public void SetPokeContainer()
        {
            // Check for IPokeContainer and set the Pokemon to it, if found
            var hits = Camera.main.CursorRaycastHits();
            var pokeContainer = hits.FirstOrDefault(h => h.collider.gameObject.TryGetComponent(out IPokeContainer container));
            if (pokeContainer.collider == null) return;
            var container = pokeContainer.collider.gameObject.GetComponent<IPokeContainer>();
            if (container != null)
            {
                container.SetPokemon(this);
                MoveTo.ReleaseCursor();
            }
            return;
        }
    }
}
