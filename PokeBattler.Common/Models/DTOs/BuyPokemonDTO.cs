using System;
using System.Collections.Generic;

namespace PokeBattler.Common.Models.DTOs
{
    public class BuyPokemonDTO
    {
        public Guid TrainerId { get; set; }
        public Pokemon Pokemon { get; set; }
        public IEnumerable<Guid> PokemonToDestroy { get; set; }
        public RefreshShopDTO Shop { get; set; }
        public MovePokemonDTO Move { get; set; }
    }
}
