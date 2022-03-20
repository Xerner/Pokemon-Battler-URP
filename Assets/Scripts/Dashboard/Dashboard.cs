using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashboard : MonoBehaviour
{
    public static int[] TierChances = { 100, 0, 0, 0, 0 };
    public int money;
    //public int TrainerLevel;
    [SerializeField] private ShopCard[] shopCards;
    [HideInInspector] public Trainer[] Trainers;

    public ShopCard[] ShopCards { get; private set; }

    private void Start()
    {
        //RefreshShop();
    }

    public void RefreshShop()
    {
        foreach (ShopCard card in shopCards)
        {
            string pokemonName = AssetManager.PokemonNames[Random.Range(0, AssetManager.PokemonNames.Length)];
            card.SetPokemon(AssetManager.Pokemon[pokemonName]);
        }
    }
}
