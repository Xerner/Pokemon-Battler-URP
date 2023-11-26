using System;
using System.Threading.Tasks;
using PokeBattler.Unity;
using PokeBattler.Models;
using UnityEngine;

namespace PokeBattler.Core
{
    public partial class Trainer
    {
        public Guid ClientID { get; set; }
        public Account Account;
        public ArenaBehaviour Arena;
        [SerializeField] TrainerCard TrainerCard;
        bool Ready = false;
        int CurrentHealth = 100;
        int TotalHealth = 100;
        int experience = 0;
        public int Money = 10;
        int level = 2;
        public int Level { get => level; }
        public int Experience { get => experience; }
        public string ExperienceStr { get => experience + "/" + LevelToExpNeededToLevelUp[level]; }

        public TrainersPokemon ActivePokemon = new TrainersPokemon();

        Action<bool> onReady;

        public void OnReadySubscribe(Action<bool> action) => onReady += action;
        public void OnReadyUnsubscribe(Action<bool> action) => onReady -= action;

        public Trainer(Account account)
        {
            Account = account;
        }

        public bool OnReady(bool ready)
        {
            Ready = ready;
            onReady?.Invoke(ready);
            return ready;
        }

        public int CalculateInterest() => Mathf.FloorToInt(Money / 10);

        /// <summary>Add experience to the Trainer. Rolls over exp if they level up.</summary>
        /// <returns>True if the player levels up</returns>
        public bool AddExperience(int expToAdd)
        {
            int expNeeded = LevelToExpNeededToLevelUp[level];
            int newExp = experience + expToAdd;
            int difference = expNeeded - newExp;
            if (difference <= 0)
            { // leveled up
              // edge case: player is already max level
                if (!LevelToExpNeededToLevelUp.ContainsKey(level + 1))
                {
                    experience = LevelToExpNeededToLevelUp[level];
                    difference = 1;
                }
                else
                {
                    level++;
                    experience = Mathf.Abs(difference);
                }
            }
            else
            {
                experience = newExp;
            }
            return difference <= 0;
        }

        public async Task<PokemonBehaviour> AddPokemonToBench(Pokemon pokemon)
        {
            BenchBehaviour bench = Arena.Bench.GetAvailableBench();
            PokemonBehaviour pokemonBehaviour = await ActivePokemon.Add(pokemon, bench);
            pokemonBehaviour.MoveTo.ShouldLerpToPosition = false;
            pokemonBehaviour.SetPokeContainer(bench);
            pokemonBehaviour.MoveTo.ShouldLerpToPosition = true;
            return pokemonBehaviour;
        }
    }
}
