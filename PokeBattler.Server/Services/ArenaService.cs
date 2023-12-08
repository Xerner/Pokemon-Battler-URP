using PokeBattler.Common.Models;
using PokeBattler.Common.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace PokeBattler.Server.Services;

public interface IArenaService
{
    public Arena AssignArena(Guid id);
    public Arena GetArena(Guid id);
    public IPokeContainer GetAvailableBench(Trainer arena);
}

public class ArenaService
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

    public IPokeContainer GetAvailableBench(Trainer trainer)
    {
        var arena = GetArena(trainer.Id);
        foreach (IPokeContainer bench in arena.Bench)
            if (bench.Pokemon == null)
                return bench;
        return null;
    }
}
