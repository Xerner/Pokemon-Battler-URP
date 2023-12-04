using PokeBattler.Common.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeBattler.Server.Models;

namespace PokeBattler.Server.Services;

public interface ITrainerService
{
    public const int PlayerCount = 8;
    Trainer GetTrainer(Guid id);
    TrainersPokemon GetTrainersPokemon(Guid id);
    Trainer Create(Account account);
}

public class TrainersService(ILogger<TrainersService> logger) : ITrainerService
{
    readonly Dictionary<Guid, Trainer> Trainers = [];
    readonly Dictionary<Guid, TrainersPokemon> TrainersPokemon = [];

    readonly ILogger<TrainersService> logger;

    public Trainer GetTrainer(Guid id)
    {
        return Trainers[id];
    }

    public TrainersPokemon GetTrainersPokemon(Guid id)
    {
        return TrainersPokemon[id];
    }

    public Trainer Create(Account account)
    {
        var trainer = new Trainer(account);
        // if trainer was already in-game and is reconnecting
        if (Trainers.ContainsKey(trainer.Id))
        {
            logger.LogInformation($"Reconnecting Trainer {account.Username} {trainer.Id}");
            throw new NotImplementedException();
        }
        // gameService slots are full
        if (Trainers.Count >= ITrainerService.PlayerCount)
        {
            logger.LogInformation($"Game is full, rejecting Trainer {account.Username} {trainer.Id}");
            throw new NotImplementedException();
        }
        logger.LogInformation($"Created Trainer {account.Username} {trainer.Id}");
        Trainers.Add(trainer.Id, trainer);
        return trainer;
    }
}
