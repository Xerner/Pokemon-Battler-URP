using AutoChess.Contracts.Models.Json;
using System.Text.Json;

namespace AutoChess.Library.Extensions;

public static class PokemonJsonExtensions
{
    public static PokemonJson? FromJson(this PokemonJson _, string json)
    {
        return JsonSerializer.Deserialize<PokemonJson>(json);
    }
}
