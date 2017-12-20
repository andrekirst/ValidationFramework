using System;

namespace ValidationFramework.Functions
{
    public static class DateFunctions
    {
        /// <summary>
        /// Checks, if the value is today
        /// </summary>
        /// <param name="value">The date to check</param>
        /// <returns>True if the date of the value is today</returns>
        public static bool IsToday(this DateTime value)
        {
            DateTime today = DateTime.Today;
            return today.Year == value.Year &&
                today.Month == value.Month &&
                today.Day == value.Day;
        }
    }
}
