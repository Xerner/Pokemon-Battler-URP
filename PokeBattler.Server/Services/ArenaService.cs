﻿using PokeBattler.Common.Extensions;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.Enums;
using PokeBattler.Common.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace PokeBattler.Server.Services;

public interface IArenaService
{
    public Arena AssignArena(Guid id);
    public Arena GetArena(Guid id);
    public int GetAvailableBenchIndex(Trainer trainer);
    public (EContainerType, int) GetContainerWithPokemonToEvolve(Trainer trainer, Pokemon pokemon);
}

public class ArenaService : IArenaService
{
    public Dictionary<Guid, Arena> Arenas { get; private set; }

    public Arena AssignArena(Guid id)
    {
        var availableArena = new Arena();
        if (availableArena != null)
        {
            availableArena.OwnerId = id;
            Arenas.Add(id, availableArena);
        }
        return availableArena;
    }

    public Arena GetArena(Guid id)
    {
        if (Arenas.ContainsKey(id))
            return Arenas[id];
        return null;
    }

    public int GetAvailableBenchIndex(Trainer trainer)
    {
        var arena = GetArena(trainer.Id);
        foreach (var (bench, i) in arena.Bench.SelectIndex())
            if (bench.Pokemon == null)
                return i;
        return -1;
    }

    public (EContainerType, int) GetContainerWithPokemonToEvolve(Trainer trainer, Pokemon pokemon)
    {
        var arena = GetArena(trainer.Id);
        foreach (var (bench, i) in arena.Bench.SelectIndex())
            if (bench.Pokemon != null && bench.Pokemon.PokeId == pokemon.PokeId)
                return (EContainerType.Bench, i);
        foreach (var (spot, i) in arena.ArenaSpots.SelectIndex())
            if (spot.Pokemon != null && spot.Pokemon.PokeId == pokemon.PokeId)
                return (EContainerType.Arena, i);
        return (EContainerType.Bench, -1);
    }
}
