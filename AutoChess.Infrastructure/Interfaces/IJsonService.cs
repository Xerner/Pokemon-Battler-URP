using AutoChess.Contracts.Models;

namespace AutoChess.Infrastructure.Interfaces;

public interface IJsonService
{
    Task<Pokemon> PokemonFromJson(string json, bool hasHiddenAbility);
}
