using PokeBattler.Common.Models;
using System.Collections.Generic;

namespace PokeBattler.Server.Models;

public class TrainerShop
{
    public bool FreeShop { get; set; }
    public IEnumerable<Pokemon> ShopPokemon { get; set; }
}
