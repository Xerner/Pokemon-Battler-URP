using UnityEngine;
using System;

public class Dashboard : MonoBehaviour {
    public static int[] TierChances = { 100, 0, 0, 0, 0 };
    public int money;
    //public int TrainerLevel;
    [SerializeField] private ShopCardUI[] shopCards;
    [HideInInspector] public Trainer[] Trainers;

    public ShopCardUI[] ShopCards { get; private set; }

    private void Start() {
        //RefreshShop();
    }

    void OnGUI() {
        //if (!btnTexture) {
        //    Debug.LogError("Please assign a texture on the inspector");
        //    return;
        //}

        if (GUI.Button(new Rect(5, 5, 200, 30), "Fetch All Pokemon"))
            Pokemon.InitializeAllPokemon();
    }

    public void RefreshShop() {
        System.Random rnd = new System.Random();
        foreach (ShopCardUI card in shopCards) {
            string pokemonName = rnd.NextEnum<EPokemonName>().ToString().ToLower();
            Pokemon.GetPokemonFromAPI(pokemonName, (pokemon) => card.SetPokemon(pokemon));
        }
    }
}
