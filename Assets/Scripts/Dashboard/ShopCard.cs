using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopCard : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image pokemonSprite;
    [SerializeField] private Image type1;
    [SerializeField] private Image type2;
    [SerializeField] private TextMeshProUGUI pokemonName;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject pokeDollar;
    public Action<ShopCard> OnClick;
    private int cost;

    public int Cost { get => cost; set => cost = value; }
    public string PokemonName { get => pokemonName.text; private set => pokemonName.text = value; }

    public void SetPokemon(PokemonBehaviour pokemon)
    {
        background.sprite = AssetManager.Singleton.ShopCardSprites[pokemon.pokemon.tier -1];
        pokemonSprite.sprite = AssetManager.PokemonSprites[pokemon.name];
        pokemonSprite.SetNativeSize();
        type1.sprite = AssetManager.PokemonTypesSprites[pokemon.pokemon.types[0].ToString()].MiniSprite;
        type2.sprite = AssetManager.PokemonTypesSprites[pokemon.pokemon.types[0].ToString()].MiniSprite;
        type1.SetNativeSize();
        type2.SetNativeSize();
        PokemonName = pokemon.name;
        pokeDollar.SetActive(true);
        cost = pokemon.pokemon.tier;
        costText.text = cost.ToString();
    }

    public void DebugBuyPokemon()
    {
        if (pokemonName.text == "") return;
        PokeContainer container = TrainerManager.ActiveTrainer.Arena.Party.NextOpen();
        if (container == null && TrainerManager.ActiveTrainer.IsAboutToEvolve(pokemonName.text))
        {
            container = TrainerManager.ActiveTrainer.ActivePokemon[pokemonName.text][0].CurrentField;
        }
        TrainerManager.ActiveTrainer.Money -= cost;
        TrainerManager.ActiveTrainer.AddPokemon(AssetManager.PokemonPrefabs[pokemonName.text].GetComponent<PokemonBehaviour>());
    }
}
