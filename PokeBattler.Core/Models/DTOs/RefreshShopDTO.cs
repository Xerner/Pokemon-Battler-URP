using System.Collections.Generic;

namespace PokeBattler.Common.Models.DTOs
{
    public class RefreshShopDTO
    {
        public int Money { get; set; }
        public IEnumerable<Pokemon> ShopPokemon { get; set; }
    }
}
