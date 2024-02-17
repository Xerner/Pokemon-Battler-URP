using System.Collections.Generic;

namespace PokeBattler.Common.Models
{
    /// <summary>
    /// {
    ///   [pokemon tier]: {
    ///     "bulbasaur": [count in pool],
    ///   }
    /// }
    /// </summary>
    public class PokemonPool : Dictionary<int, Dictionary<string, int>>
    {
        public void Initialize(int[] tierCounts)
        {
            foreach (var item in this)
            {
                Remove(item.Key);
            }
            foreach (var item in tierCounts)
            {
                Add(item, []);
            }
        }
    }
}
