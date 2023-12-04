using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Zenject;
using System;
using PokeBattler.Client.Services;
using PokeBattler.Common.Models;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Dashboard")]
    public class DashboardBehaviour : MonoInstaller<DashboardBehaviour>
    {
        [Obsolete("Refactor to use DashboardService")]

        [SerializeField] TextMeshProUGUI trainerLevel;
        [SerializeField] TextMeshProUGUI experience;
        [SerializeField] TextMeshProUGUI money;
        [SerializeField] ShopCardBehaviour[] shopCards;
        [SerializeField] TextMeshProUGUI[] tierChances;
        [HideInInspector] public Trainer[] Trainers;

        public ShopCardBehaviour[] ShopCards { get; private set; }
        Pokemon[] pokemons;
        const int shopCost = 2;
        const int experienceCost = 4;

        private IGameService gameService;
        private ITrainersService trainersService;

        public string Level { get { return trainerLevel.text; } set { trainerLevel.text = value; } }
        public string Experience { get { return experience.text; } set { experience.text = value; } }
        public string Money { get { return money.text; } set { money.text = value; } }

        void OnGUI()
        {
            //if (GUI.Button(new Rect(Screen.width - 205, 5, 200, 30), "Fetch All PokemonGO"))
            //{
            //    PokemonGO.InitializeAllPokemon((pokemon) => { if (PokemonGO.CachedPokemon.Keys.Count == PokemonGO.TotalPokemon) RefreshShop(); });
            //}
            if (GUI.Button(new Rect(Screen.width - 205, 40, 200, 30), "Fetch 5 PokemonGO"))
            {
                Pokemon.InitializeListOfPokemon(
                    new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "geodude" },
                    (pokemon) => { if (Pokemon.CachedPokemon.Keys.Count == 5) RefreshShop(false); }
                );
            }
        }

        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle();
        }

        [Inject]
        public void Construct(IGameService gameService, ITrainersService trainersService)
        {
            this.gameService = gameService;
            this.trainersService = trainersService;
        }

        /// <summary>Sets the level, experience, and money elements in the DashboardBehaviour based on the provided trainer</summary>
        public void SetTrainer(Trainer trainer)
        {
            Level = trainer.Level.ToString();
            Experience = trainer.ExperienceStr;
            Money = trainer.Money.ToString();
            SetTierChances(trainer.Level);
        }

        private void SetTierChances(int level)
        {
            for (int i = 0; i < 5; i++)
            {
                tierChances[i].text = PokemonPool.Constants.TierChances[level, i].ToString();
            }
        }

        /// <summary>ActiveTrainer attempts to buy experience. Updates UI and trainer variables accordingly</summary>
        public void BuyExperience()
        {
            if (!gameService.Game.GameSettings.FreeExperience)
            {
                if (trainersService.ClientsTrainer.Money < experienceCost)
                {
                    Debug2.Log("Not enough money to buy experience!");
                    return;
                }
                trainersService.ClientsTrainer.Money -= experienceCost;
                money.text = trainersService.ClientsTrainer.Money.ToString();
            }
            if (trainersService.ClientsTrainer.AddExperience(experienceCost))
            {
                trainerLevel.text = trainersService.ClientsTrainer.Level.ToString();
                SetTierChances(trainersService.ClientsTrainer.Level);
            }
            experience.text = trainersService.ClientsTrainer.ExperienceStr;
        }

        /// <summary>ActiveTrainer attempts to refresh the entire shop. Updates UI and trainer variables accordingly</summary>
        public void RefreshShop(bool subtractMoney = true)
        {
            if (!gameService.Game.GameSettings.FreeRefreshShop)
            {
                if (trainersService.ClientsTrainer.Money < shopCost)
                {
                    Debug2.Log("Not enough money to refresh the shop!");
                    return;
                }
                if (subtractMoney) trainersService.ClientsTrainer.Money -= shopCost;
                money.text = trainersService.ClientsTrainer.Money.ToString();
            }
            if (this.pokemons != null) gameService.Game.PokemonPool.Refund(this.pokemons);
            Pokemon[] pokemons = gameService.Game.PokemonPool.Withdraw5(trainersService.ClientsTrainer.Level);
            for (int i = 0; i < pokemons.Length; i++)
                shopCards[i].SetPokemon(pokemons[i], pokemons[i].tier);
            this.pokemons = pokemons;
        }
    }
}
