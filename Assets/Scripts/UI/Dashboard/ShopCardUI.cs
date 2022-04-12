using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopCardUI : MonoBehaviour
{
    #region UI stuff
    [SerializeField] private TextMeshProUGUI pokemonName;
    [SerializeField] private Image pokemonSprite;
    [SerializeField] private Image background;
    [SerializeField] private Image type1;
    [SerializeField] private Image type2;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject pokeDollar;
    #endregion

    public Action<ShopCardUI> OnClick;
    private int cost;

    public int Cost { get => cost; set => cost = value; }
    public string PokemonName { get => pokemonName.text; private set => pokemonName.text = value; }

    public void SetPokemon(Pokemon pokemon)
    {
        background.sprite = StaticAssets.ShopCardSprites[(pokemon.tier).ToString()];
        pokemonSprite.sprite = pokemon.shopSprite;
        pokemonSprite.color = new Color(pokemonSprite.color.r, pokemonSprite.color.g, pokemonSprite.color.b, 1f);
        pokemonSprite.SetNativeSize();
        type1.sprite = StaticAssets.typeMiniSprites[pokemon.types[0].ToString()];
        type1.SetNativeSize();
        if (pokemon.types[1] == EPokemonType.None) {
            type2.sprite = null;
            type2.color = new Color(1f,1f,1f,0f);
        } else {
            type2.sprite = StaticAssets.typeMiniSprites[pokemon.types[1].ToString()];
            type2.SetNativeSize();
            type2.color = new Color(1f,1f,1f,1f);
        }
        PokemonName = pokemon.name;
        pokeDollar.SetActive(true);
        cost = pokemon.tier;
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
        //TrainerManager.ActiveTrainer.AddPokemon(StaticAssets.PokemonPrefabs[pokemonName.text].GetComponent<PokemonBehaviour>());
    }
}
