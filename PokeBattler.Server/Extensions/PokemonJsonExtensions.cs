using Newtonsoft.Json;
using PokeBattler.Common.Models.Json;

namespace PokeBattler.Server.Extensions;

public static class PokemonJsonExtensions
{
    public static PokemonJson FromJson(this PokemonJson _, string json)
    {
        return JsonConvert.DeserializeObject<PokemonJson>(json);
    }
}
