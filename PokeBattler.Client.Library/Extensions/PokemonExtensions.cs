using AutoChess.Contracts.Enums;
using AutoChess.Contracts.Models;

namespace AutoChess.Library.Extensions
{
    public static class PokemonExtensions
    {
        public static string EvolutionsToString(this Pokemon pokemon)
        {
            string str = "";
            for (int i = 0; i < pokemon.Evolutions.Count; i++)
            {
                if (i == pokemon.Evolutions.Count - 1) str += pokemon.Evolutions[i];
                else str += pokemon.Evolutions[i] + " → ";
            }
            return str;
        }

        public static string TypeToString(this Pokemon pokemon)
        {
            string str = "";
            for (int i = 0; i < pokemon.Types.Length; i++)
            {
                if (pokemon.Types[i] == EPokemonType.None) continue;
                if (i == pokemon.Types.Length - 1) str += pokemon.Types[i];
                else str += pokemon.Types[i] + "    ";
            }
            return str;
        }
    }
}
