using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopCardUI : MonoBehaviour {
    #region UI stuff
    [SerializeField] private TextMeshProUGUI pokemonName;
    [SerializeField] private Image pokemonSprite;
    [SerializeField] private Image background;
    [SerializeField] private Image type1;
    [SerializeField] private Image type2;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject pokeDollar;
    #endregion

    private Pokemon pokemon;
    public Action<ShopCardUI> OnClick;
    public int Cost { get => pokemon.tier; }

    public void SetPokemon(Pokemon pokemon) {
        background.sprite = StaticAssets.ShopCardSprites[(pokemon.tier).ToString()];
        pokemonSprite.sprite = pokemon.ShopSprite;
        pokemonSprite.color = new Color(pokemonSprite.color.r, pokemonSprite.color.g, pokemonSprite.color.b, 1f);
        pokemonSprite.SetNativeSize();
        type1.sprite = StaticAssets.typeMiniSprites[pokemon.types[0].ToString()];
        type1.color = new Color(1f, 1f, 1f, 1f);
        type1.SetNativeSize();
        if (pokemon.types[1] == EPokemonType.None) {
            type2.sprite = null;
            type2.color = new Color(1f, 1f, 1f, 0f);
        } else {
            type2.sprite = StaticAssets.typeMiniSprites[pokemon.types[1].ToString()];
            type2.SetNativeSize();
            type2.color = new Color(1f, 1f, 1f, 1f);
        }
        this.pokemon = pokemon;
        pokeDollar.SetActive(true);
        costText.text = pokemon.tier.ToString();
    }

    public void TryToBuy() {
        if (pokemon == null) return;
        if (TrainerManager.Instance.ActiveTrainer.Money < Cost) {
            Debug2.Log("Not enough money", LogLevel.Detailed);
            return; 
        }
        PokeContainer container = TrainerManager.Instance.ActiveTrainer.Arena.Bench.GetAvailableBench();
        // Trainer bought a Pokemon, but the bench is full, BUT the purchased Pokemon can be used in an evolution to free up space
        if (BenchHasValidContainer(container))
            container = TrainerManager.Instance.ActiveTrainer.ActivePokemon[pokemonName.text][0].CurrentField;
        else
            return;
        TrainerManager.Instance.ActiveTrainer.Money -= Cost;
        Debug2.Log($"bought a {pokemon.name} for {pokemon.tier}", LogLevel.Detailed);
        TrainerManager.Instance.ActiveTrainer.AddPokemonToBench(PokemonBehaviour.Spawn(pokemon));
    }

    private bool BenchHasValidContainer(PokeContainer container) {
        return container != null || container == null && TrainerManager.Instance.ActiveTrainer.IsAboutToEvolve(pokemon);
    }
}
