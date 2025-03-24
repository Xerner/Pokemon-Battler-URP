using System;
using AutoChess.Contracts.Enums;

namespace AutoChess.Contracts.DTOs
{
    public class MoveUnitDTO : BaseDTO
    {
        public Guid TrainerId { get; set; }
        public Guid PokemonId { get; set; }
        public int PokeContainerIndex { get; set; }
        public EContainerType ContainerType { get; set; }
    }
}
