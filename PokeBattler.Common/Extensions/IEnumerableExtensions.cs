using System.Collections.Generic;
using System.Linq;

namespace PokeBattler.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<(T, int)> SelectIndex<T>(this IEnumerable<T> values)
        {
            return values.Select((value, i) => (value, i));
        }
    }
}
