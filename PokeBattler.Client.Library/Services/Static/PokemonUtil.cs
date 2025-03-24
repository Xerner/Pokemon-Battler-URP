using System.Collections.Generic;
using AutoChess.Contracts.Enums;

namespace AutoChess.Library.Models
{
    public class PokemonUtil
    {
        private static Dictionary<string, EPokemonType> stringToType = new Dictionary<string, EPokemonType>() {
        { "none", EPokemonType.None },
        { "mystery", EPokemonType.Mystery },
        { "bug", EPokemonType.Bug },
        { "dark", EPokemonType.Dark },
        { "dragon", EPokemonType.Dragon },
        { "electric", EPokemonType.Electric },
        { "fairy", EPokemonType.Fairy },
        { "fighting", EPokemonType.Fighting },
        { "fire", EPokemonType.Fire },
        { "flying", EPokemonType.Flying },
        { "ghost", EPokemonType.Ghost },
        { "grass", EPokemonType.Grass },
        { "ground", EPokemonType.Ground },
        { "ice", EPokemonType.Ice },
        { "normal", EPokemonType.Normal },
        { "poison", EPokemonType.Poison },
        { "psychic", EPokemonType.Psychic },
        { "rock", EPokemonType.Rock },
        { "steel", EPokemonType.Steel },
        { "water", EPokemonType.Water }
    };

        public static EPokemonType StringToType(string type) => stringToType[type];
    }
}
