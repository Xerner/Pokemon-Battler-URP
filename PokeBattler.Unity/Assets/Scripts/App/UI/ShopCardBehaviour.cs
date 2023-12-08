using PokeBattler.Client.Services;
using PokeBattler.Common;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.Enums;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PokeBattler.Unity {
    [AddComponentMenu("Poke Battler/Shop Card")]
    [RequireComponent(typeof(Image))]
    public class ShopCardBehaviour : MonoBehaviour
    {
        #region UI stuff
        Image background;
        [SerializeField] private TextMeshProUGUI pokemonName;
        [SerializeField] private Image pokemonImage;
        [SerializeField] private Image type1;
        [SerializeField] private Image type2;
        [SerializeField] private UnityEngine.GameObject pokeDollar;
        [SerializeField] private TextMeshProUGUI costText;
        #endregion

        private Pokemon pokemon;
        public Action<ShopCardBehaviour> OnClick;
        public int Cost { get; set; }

        private ITrainersService trainersService;
        private IShopService shopService;

        [Inject]
        public void Construct(ITrainersService trainersService, IShopService shopService)
        {
            this.trainersService = trainersService;
            this.shopService = shopService;
        }

        void Start()
        {
            background = GetComponent<Image>();
            Reset();
        }

        public void SetPokemon(Pokemon pokemon, int cost)
        {
            pokemonName.text = pokemon.name;
            Cost = cost;
            background.sprite = StaticAssets.ShopCardSprites[Cost.ToString()];
            pokemonImage.sprite = pokemon.ShopSprite;
            pokemonImage.color = new Color(pokemonImage.color.r, pokemonImage.color.g, pokemonImage.color.b, 1f);
            pokemonImage.SetNativeSize();
            type1.sprite = StaticAssets.typeMiniSprites[pokemon.types[0].ToString()];
            type1.color = new Color(1f, 1f, 1f, 1f);
            type1.SetNativeSize();
            if (pokemon.types[1] == EPokemonType.None)
            {
                type2.sprite = null;
                type2.color = new Color(1f, 1f, 1f, 0f);
            }
            else
            {
                type2.sprite = StaticAssets.typeMiniSprites[pokemon.types[1].ToString()];
                type2.SetNativeSize();
                type2.color = new Color(1f, 1f, 1f, 1f);
            }
            this.pokemon = pokemon;
            pokeDollar.SetActive(true);
            costText.text = Cost.ToString();
        }

        public void Reset()
        {
            pokemonName.text = "";
            pokemonImage.sprite = null;
            pokemonImage.color = new Color(1f, 1f, 1f, 0f);
            type1.sprite = null;
            type1.color = new Color(1f, 1f, 1f, 0f);
            type2.sprite = null;
            type2.color = new Color(1f, 1f, 1f, 0f);
            pokemon = null;
            pokeDollar.SetActive(false);
            costText.text = "";
        }

        public void BuyPokemon()
        {
            if (pokemon == null) return;
            shopController.RequestToBuyPokemon(pokemon);
        }
    }
}
