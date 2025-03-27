using AutoChess.Contracts.Models;

namespace AutoChess.Library.Services;

public interface IArenaService
{
    public Arena? AssignArena(Guid id);
    public Arena? GetArena(Guid id);
}

public class ArenaService : IArenaService
{
    public Dictionary<Guid, Arena> Arenas { get; private set; } = [];

    public Arena? AssignArena(Guid id)
    {
        var availableArena = new Arena();
        if (availableArena != null)
        {
            availableArena.OwnerId = id;
            Arenas.Add(id, availableArena);
        }
        return availableArena;
    }

    public Arena? GetArena(Guid id)
    {
        if (Arenas.TryGetValue(id, out Arena? value))
            return value;
        return null;
    }
}
