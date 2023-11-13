using Poke.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Poke.Unity {
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
        public int Cost { get => pokemon.tier; }

        void Start()
        {
            background = GetComponent<Image>();
            Reset();
        }

        public void SetPokemon(Pokemon pokemon)
        {
            pokemonName.text = pokemon.name;
            background.sprite = StaticAssets.ShopCardSprites[pokemon.tier.ToString()];
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
            costText.text = pokemon.tier.ToString();
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

        public void TryToBuy()
        {
            if (pokemon == null) return;
            if (HostBehaviour.Instance.Host.Trainer.Money < Cost && !HostBehaviour.Instance.GameSettings.FreePokemon)
            {
                Debug2.Log("Not enough money", LogLevel.Detailed);
                return;
            }
            BenchBehaviour bench = HostBehaviour.Instance.Host.Trainer.Arena.Bench.GetAvailableBench();
            // Trainer bought a Pokemon, but the bench is full, BUT the purchased Pokemon can be used in an evolution to free up space
            if (!BenchHasValidContainer(bench))
                return;
            //else
            //container = TrainerManager.ActiveTrainer.ActivePokemon[pokemon.name][0].MoveTo;
            HostBehaviour.Instance.Host.Trainer.Money -= Cost;
            DashboardBehaviour.Instance.Money = HostBehaviour.Instance.Host.Trainer.Money.ToString();
            Debug2.Log($"bought a {pokemon.name} for {pokemon.tier}", LogLevel.Detailed);
            HostBehaviour.Instance.Host.Trainer.AddPokemonToBench(PokemonBehaviour.Spawn(pokemon));
            Reset();
        }

        private bool BenchHasValidContainer(BenchBehaviour bench)
        {
            return bench != null || (bench == null && HostBehaviour.Instance.Host.Trainer.IsAboutToEvolve(pokemon));
        }
    }
}
