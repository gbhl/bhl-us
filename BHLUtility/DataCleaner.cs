using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MOBOT.BHL.Utility
{
    public static class DataCleaner
    {
        /// <summary>
        /// Transform the specified Year value to one of the following forms:
        ///     XXXX
        ///     XXXX-XXXX
        ///     XXXX,XXXX
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static string CleanYear(string year)
        {
            if (!string.IsNullOrWhiteSpace(year))
            {
                // Remove trailing periods
                year = (year.Substring(year.Length - 1) == ".") ? year.Substring(0, year.Length - 1) : year;

                // Remove single question marks from year values greater than four characters.
                // This rule cleans values like 1970?, but leaves 19??-1980 and 18?? alone.
                if (!year.Contains("??") && year.Length > 4) year = year.Replace("?", "");

                // Remove parentheses
                year = year.Replace("(", "");
                year = year.Replace(")", "");

                // Remove 'orphaned' brackets.  (In other words, a [ without a ], or a ] without a [.)
                if (year.Contains("[") && !year.Contains("]")) year = year.Replace("[", "");
                if (year.Contains("]") && !year.Contains("[")) year = year.Replace("]", "");

                // Remove brackets at the beginning and end of the year value
                if (year.StartsWith("[") && year.EndsWith("]")) year = year.Replace("[", "").Replace("]", "");

                // Remove spaces before and after hyphens
                year = year.Replace(" - ", "-");
                year = year.Replace(" -", "-");
                year = year.Replace("- ", "-");

                // Remove colons
                year = year.Replace(":", "");

                // Remove spaces following commas
                year = year.Replace(", ", ",");

                // Remove leading and trailing hyphens from valid year values such as 1970--, 1980-, and -1990.
                // This rule does not affect values like 19-- and 197-.
                year = year.EndsWith("--") && year.Length >= 6 ? year.Substring(0, year.Length - 2) : year;
                year = year.EndsWith("-") && !year.EndsWith("--") && year.Length > 4 ? year.Substring(0, year.Length - 1) : year;
                year = year.Length > 1 ? year.StartsWith("-") ? year.Substring(1) : year : year;

                // Removing trailing forward slashes.
                year = year.EndsWith("/") ? year.Substring(0, year.Length - 1) : year;

                year = year.Trim();

                // Expand XXXX-XX values to XXXX-XXXX
                Regex regex = new Regex("^[0-9]{4}-[0-9]{2}$");
                Match match = regex.Match(year);
                if (match.Success) year = year.Substring(0, 5) + year.Substring(0, 2) + year.Substring(5);
            }

            return year;
        }
    }
}
