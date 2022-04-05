using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PokemonBehaviour : MonoBehaviour {
    private SpriteRenderer sprite;
    private Animator animator;

    // TODO: create an Attribute to print the values of pokemon as uneditable labels
    [SerializeField][Pokemon]
    private Pokemon pokemon;
    [SerializeField] 
    public PokemonCombat Combat;
    //public bool isSelected;
    public PokeContainer CurrentField;
    public Trainer trainer;

    public Action<PokemonBehaviour> OnDestroyed;

    [Header("Pokemon API")]
    [SerializeField]
    private string idOrName;

    public Pokemon Pokemon { get; private set; }

    private void Start() {
        InitializeComponents();
    }

    private void InitializeComponents() {
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    public void Initialize() => Initialize(idOrName);

    public void Initialize(string idOrName) {
        InitializeComponents();
        if (idOrName.All(char.IsDigit) || Pokemon.GetValidPokemonName(idOrName) != "") {
            Pokemon.GetPokemonFromAPI(idOrName, (pokemon) => {
                this.pokemon = pokemon;
                sprite.sprite = pokemon.sprite;
                gameObject.name = pokemon.name;
            });
        } else {
            Debug.LogError("Invalid pokemon ID or name given: " + idOrName);
        }
    }

    private void OnDestroy() {
        //if (CurrentField == null) throw new System.Exception($"{id}\t{name} : CurrentField == null!");
        CurrentField?.Reset();
        OnDestroyed?.Invoke(this);
    }

    public PokemonBehaviour Evolve() {
        ////PokemonBehaviour evolution = Instantiate(pokemon.evolution, transform).GetComponent<PokemonBehaviour>();
        //evolution.id = id;
        ////Network.IDtoPokemon[id] = evolution;
        //Destroy(this);
        //CurrentField.SetPokemon(evolution);
        //return evolution;
        return null;
    }
}
