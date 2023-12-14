using PokeBattler.Common.Models.Enums;
using System;

namespace PokeBattler.Common.Models.DTOs
{
    public class MovePokemonDTO
    {
        public Guid TrainerId { get; set; }
        public Guid PokemonId { get; set; }
        public int PokeContainerIndex { get; set; }
        public EContainerType ContainerType { get; set; }
    }
}
