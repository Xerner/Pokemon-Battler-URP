using UnityEngine;
using System.Collections.Generic;

public class Dashboard : MonoBehaviour {
    public static int[] TierChances = { 100, 0, 0, 0, 0 };
    public int money;
    //public int TrainerLevel;
    [SerializeField] private ShopCardUI[] shopCards;
    [HideInInspector] public Trainer[] Trainers;

    public ShopCardUI[] ShopCards { get; private set; }
    private Pokemon[] pokemons;

    private void Start() {
        Pokemon.InitializeListOfPokemon(new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "geodude" },
                (pokemon) => { if (Pokemon.CachedPokemon.Keys.Count == 5) RefreshShop(); }
            );
    }

    void OnGUI() {
        //if (!btnTexture) {
        //    Debug.LogError("Please assign a texture on the inspector");
        //    return;
        //}

        if (GUI.Button(new Rect(Screen.width - 205, 5, 200, 30), "Fetch All Pokemon"))
            Pokemon.InitializeAllPokemon((pokemon) => { if (Pokemon.CachedPokemon.Keys.Count == Pokemon.TotalPokemon) RefreshShop(); });
        if (GUI.Button(new Rect(Screen.width - 205, 40, 200, 30), "Fetch 5 Pokemon"))
            Pokemon.InitializeListOfPokemon(new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "geodude" },
                (pokemon) => { if (Pokemon.CachedPokemon.Keys.Count == 5) RefreshShop(); }
            );
    }
    
    public void RefreshShop() {
        if (this.pokemons != null) PokemonPool.Instance.Refund(this.pokemons);
        Pokemon[] pokemons = PokemonPool.Instance.Withdraw5();
        for (int i = 0; i < pokemons.Length; i++)
            shopCards[i].SetPokemon(pokemons[i]);
        this.pokemons = pokemons;
    }
}
