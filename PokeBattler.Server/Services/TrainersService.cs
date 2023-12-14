using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using PokeBattler.Common.Models;
using PokeBattler.Server.Services.Hubs;
using PokeBattler.Common.Models.Interfaces;
using PokeBattler.Server.Extensions;
using PokeBattler.Server.Models;

namespace PokeBattler.Server.Services;

public interface ITrainersService
{
    public Dictionary<Guid, TrainersPokemon> TrainersPokemon { get; }
    public Dictionary<int, int> LevelToExpNeededToLevelUp { get; set; }
    public const int PlayerCount = 8;
    public Trainer GetTrainer(Guid id);
    public Trainer Create(Account account);
    public bool AddExperience(Trainer trainer, int expToAdd);
}

public class TrainersService(ILogger<TrainersService> logger, 
                             IHubContext<GameHub> hub) : ITrainersService
{
    public readonly int baseIncome = 5;
    public readonly int pvpWinIncome = 1;
    public Dictionary<int, int> winStreakIncome = new Dictionary<int, int>() {
        { 1, 0 },
        { 2, 1 },
        { 3, 1 },
        { 4, 2 },
        { 5, 3 }
    };
    public Dictionary<int, int> LevelToExpNeededToLevelUp { get; set; } = new Dictionary<int, int>() {
        { 1, 0 },
        { 2, 2 },
        { 3, 6 },
        { 4, 10 },
        { 5, 20 },
        { 6, 36 },
        { 7, 56 },
        { 8, 80 },
        { 9, 100 }
    };

    readonly Dictionary<Guid, Trainer> Trainers = [];
    public Dictionary<Guid, TrainersPokemon> TrainersPokemon { get; set; } = [];

    readonly ILogger<TrainersService> logger;

    public async Task UpdateTrainerReady(Guid id, bool ready)
    {
        await hub.Clients.All.SendAsync("UpdateTrainerReady", id, ready);
    }

    public Trainer GetTrainer(Guid id)
    {
        return Trainers[id];
    }

    public Trainer Create(Account account)
    {
        var trainer = new Trainer(account, LevelToExpNeededToLevelUp[1]);
        // if trainer was already in-game and is reconnecting
        if (Trainers.ContainsKey(trainer.Id))
        {
            logger.LogInformation($"Reconnecting Trainer {account.Username} {trainer.Id}");
            throw new NotImplementedException();
        }
        // gameService slots are full
        if (Trainers.Count >= ITrainersService.PlayerCount)
        {
            logger.LogInformation($"Game is full, rejecting Trainer {account.Username} {trainer.Id}");
            throw new NotImplementedException();
        }
        logger.LogInformation($"Created Trainer {account.Username} {trainer.Id}");
        Trainers.Add(trainer.Id, trainer);
        return trainer;
    }

    public int CalculateInterest(Trainer trainer) => trainer.Money / 10;

    /// <summary>Create experience to the Trainer. Rolls over exp if they level up.</summary>
    /// <returns>True if the player levels up</returns>
    public bool AddExperience(Trainer trainer, int expToAdd)
    {
        int expNeeded = LevelToExpNeededToLevelUp[trainer.Level];
        int newExp = trainer.Experience + expToAdd;
        int difference = expNeeded - newExp;
        if (difference <= 0)
        { // leveled up
          // edge case: player is already max level
            if (!LevelToExpNeededToLevelUp.ContainsKey(trainer.Level + 1))
            {
                trainer.Experience = LevelToExpNeededToLevelUp[trainer.Level];
                difference = 1;
            }
            else
            {
                trainer.Level++;
                trainer.Experience = Math.Abs(difference);
            }
        }
        else
        {
            trainer.Experience = newExp;
        }
        var leveledUp = difference <= 0;
        return leveledUp;
    }

    /// <summary>
    /// Adds a Pokemon to the Trainer's active Pokemon if there is room on the bench, or a species is about to evolve
    /// </summary>
    /// <param name="pokemon">The Pokemon to attempt to add to the bench</param>
    /// <param name="bench">The bench spot to add it to</param>
    /// <returns>The PokemonBehaviour instance if the Pokemon was added successfully</returns>
    public Pokemon AddPokemon(Trainer trainer, Pokemon pokemon, IPokeContainer bench)
    {
        bool evolving = TrainersPokemon[trainer.Id].IsAboutToEvolve(pokemon);
        if (bench == null && !evolving)
        {
            return null; // no fucking room
                         // evolve and/or set the PokemonBehaviour inside the container
        }
        //PokemonBehaviour pokemonBehaviour = PokemonBehaviour.Spawn(pokemon);
        TrainersPokemon[trainer.Id].Evolve(pokemon);
        // Create it to the Trainers ActivePokemon dictionary
        if (!TrainersPokemon[trainer.Id].ContainsKey(pokemon.name))
        {
            TrainersPokemon[trainer.Id].Add(pokemon.name, new List<Pokemon>());
        }
        TrainersPokemon[trainer.Id][pokemon.name].Add(pokemon);
        //pokemonBehaviour.OnDestroyed += R;
        return pokemon;
    }
}
