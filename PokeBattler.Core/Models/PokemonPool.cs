using System.Collections.Generic;

namespace PokeBattler.Common.Models
{
    public class PokemonPool
    {
        /// <summary>The count of each Pokemon left in a game</summary>
        public Dictionary<int, Dictionary<string, int>> TierToPokemonCounts = new Dictionary<int, Dictionary<string, int>>() {
            { 1, new Dictionary<string, int>() },
            { 2, new Dictionary<string, int>() },
            { 3, new Dictionary<string, int>() },
            { 4, new Dictionary<string, int>() },
            { 5, new Dictionary<string, int>() }
        };
    }
}
