using System.Linq;
using System.Collections.Generic;

namespace ValidationFramework.Functions
{
    public static class ListFunctions
    {
        public static bool In<T>(this T value, IEnumerable<T> values)
        {
            return values.Contains(value: value);
        }
    }
}
