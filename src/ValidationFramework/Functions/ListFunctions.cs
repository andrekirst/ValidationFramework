using System.Linq;
using System.Collections.Generic;

namespace ValidationFramework.Functions
{
    public static class ListFunctions
    {
        /// <summary>
        /// Is the value in a list
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="value">The value to check</param>
        /// <param name="values">The list to check</param>
        /// <returns>True if the list contains the value. Else false.</returns>
        public static bool In<T>(
            this T value,
            IEnumerable<T> values)
        {
            return values.Contains(value: value);
        }
    }
}
