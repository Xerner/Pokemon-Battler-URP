using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pokemon;
using JsonModel;

public class PokemonBehaviour : MonoBehaviour {
    public Pokemon pokemon;
    public int id;
    public new string name;
    [HideInInspector] public PokeContainer CurrentField;
    [HideInInspector] public Trainer trainer;
    //public bool isSelected;
    public bool combatMode;
    public ArenaCard combatField;
    public Allegiance Allegiance;
    private PokemonBehaviour targetEnemy;
    //private Pathing Path;
    public bool invulnerable;

    public Action<PokemonBehaviour> OnDestroyed;

    private void Start() {
        foreach (int i in Enum.GetValues(typeof(PokemonConstants.PokemonName))) {
            print(i.ToString());
            //WebRequests.Get<string>($"https://pokeapi.co/api/v2/pokemon/{name}/", (error) => Debug.LogError(error), (json) => Debug.Log(JsonToStats(json)));
        }
        return;
        if (name != "") {
            WebRequests.Get<string>($"https://pokeapi.co/api/v2/pokemon/{name}/", (error) => Debug.LogError(error), (json) => Debug.Log(JsonToStats(json)));
        } else if (id > 0) {
            WebRequests.Get<string>($"https://pokeapi.co/api/v2/pokemon/{id}/", (error) => Debug.LogError(error), (json) => Debug.Log(JsonToStats(json)));
        } else {
            Debug.LogError("Pokemon instantiated without a name or id!");
        }
    }

    private void OnDestroy() {
        //if (CurrentField == null) throw new System.Exception($"{id}\t{name} : CurrentField == null!");
        CurrentField.Reset();
        OnDestroyed?.Invoke(this);
    }

    PokemonJsonModel JsonToStats(string json) {
        PokemonJsonModel pokemon = JsonConvert.DeserializeObject<PokemonJsonModel>(json);
        return pokemon;
    }

    public Pokemon PokemonFromJson() {
        return null;
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
