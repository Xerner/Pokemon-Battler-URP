using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Zenject;
using System;
using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Dashboard")]
    [Obsolete("Refactor to use DashboardService")]
    public class DashboardBehaviour : MonoInstaller<DashboardBehaviour>
    {

        [SerializeField] TextMeshProUGUI trainerLevel;
        [SerializeField] TextMeshProUGUI experience;
        [SerializeField] TextMeshProUGUI money;
        [SerializeField] ShopCardBehaviour[] shopCards;
        [SerializeField] TextMeshProUGUI[] tierChances;
        [HideInInspector] public Trainer[] Trainers;

        public ShopCardBehaviour[] ShopCards { get; private set; }
        Pokemon[] pokemons;

        private HubConnection connection;
        private ITrainersService trainersService;
        private IPokemonPoolService pokemonPoolService;
        private IShopService shopService;

        public string Level { get { return trainerLevel.text; } set { trainerLevel.text = value; } }
        public string Experience { get { return experience.text; } set { experience.text = value; } }
        public string Money { get { return money.text; } set { money.text = value; } }

        void OnGUI()
        {
            //if (GUI.Button(new Rect(Screen.width - 205, 5, 200, 30), "Fetch All PokemonGO"))
            //{
            //    PokemonGO.InitializeAllPokemon((pokemon) => { if (PokemonGO.CachedPokemon.Keys.Count == PokemonGO.TotalPokemon) RefreshShop(); });
            //}
            //if (GUI.Button(new Rect(Screen.width - 205, 40, 200, 30), "Fetch 5 PokemonGO"))
            //{
            //    Pokemon.InitializeListOfPokemon(
            //        new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "geodude" },
            //        (pokemon) => { if (Pokemon.CachedPokemon.Keys.Count == 5) RefreshShop(false); }
            //    );
            //}
        }

        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle();
        }

        [Inject]
        public void Construct(HubConnection connection,
                              ITrainersService trainersService,
                              IShopService shopService,
                              IPokemonPoolService pokemonPoolService)
        {
            this.connection = connection;
            this.trainersService = trainersService;
            this.pokemonPoolService = pokemonPoolService;
            this.shopService = shopService;
        }

        /// <summary>Sets the level, experience, and money elements in the DashboardBehaviour based on the provided trainer</summary>
        public void SetTrainer(Trainer trainer)
        {
            Level = trainer.Level.ToString();
            Experience = trainer.ExperienceStr;
            Money = trainer.Money.ToString();
            pokemonPoolService.GetTierChances(trainer.Level);
        }

        private void SetTierChances(IEnumerable<int> tierChances_)
        {
            foreach (var (tierChance, i) in tierChances_.Select((v, i) => (v, i)))
            {
                tierChances[i].text = tierChance.ToString();
            }
        }

        /// <summary>ActiveTrainer attempts to buy experience. Updates UI and trainer variables accordingly</summary>
        public async Task BuyExperience()
        {
            var dto = await shopService.BuyExperience();
            SetTierChances(dto.TierChances);
            money.text = dto.Money.ToString();
            trainerLevel.text = dto.Level.ToString();
            experience.text = trainersService.ClientsTrainer.ExperienceStr;
        }

        /// <summary>ActiveTrainer attempts to refresh the entire shop. Updates UI and trainer variables accordingly</summary>
        public async Task RefreshShop()
        {
            var dto = await shopService.RefreshShop();
            pokemons = dto.ShopPokemon.ToArray();
            money.text = dto.Money.ToString();
            foreach (var (shopCard, i) in shopCards.Select((v,i) => (v,i)))
            {
                shopCard.SetPokemon(pokemons[i], pokemons[i].tier);
            }
        }
    }
}
