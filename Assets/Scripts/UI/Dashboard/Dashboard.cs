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

    public void RefreshShop() {
        System.Random rnd = new System.Random();
        foreach (ShopCardUI card in shopCards) {
            string pokemonName = rnd.NextEnum<EPokemonName>().ToString().ToLower();
            Pokemon.GetPokemonFromAPI(pokemonName, (pokemon) => card.SetPokemon(pokemon));
        }
    }
}
