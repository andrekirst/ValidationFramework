using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ValidationFramework.Functions
{
    public static class StringFunctions
    {
        public static bool MatchesRegexp(this string value, string pattern)
        {
            return Regex.IsMatch(
                input: value,
                pattern: pattern);
        }
    }
}
