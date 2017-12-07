using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ValidationFramework.Functions
{
    public static class StringFunctions
    {
        /// <summary>
        /// Matches a regular expression
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="pattern">The regular expression pattern</param>
        /// <exception cref="ArgumentNullException">Throws <see cref="ArgumentNullException"/> if the value or expression is null</exception>
        /// <returns>Returns true, if the value is valid. Else false</returns>
        public static bool MatchesRegexp(this string value, string pattern)
        {
            return Regex.IsMatch(
                input: value,
                pattern: pattern);
        }

        /// <summary>
        /// Checks whether the string is null or empty
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>Returns true if the input value is null or empty</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value: value);
        }

        /// <summary>
        /// Checkts whether the string is empty
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>Returns true if the input value is empty</returns>
        public static bool IsEmpty(this string value)
        {
            return value == string.Empty;
        }

        public static bool IsEMail(this string input)
        {
            if (input == null)
            {
                return false;
            }

            return input.MatchesRegexp(pattern: @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
        }
    }
}
