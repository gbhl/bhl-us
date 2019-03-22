using System;
using System.Text.RegularExpressions;

namespace MOBOT.BHL.Utility
{
    public static class DataCleaner
    {
        /// <summary>
        /// Transform the specified Year value to one of the following forms:
        ///     YYYY
        ///     YYYY-YYYY
        ///     YYYY,YYYY
        /// Extra characters and spaces are removed from the sumbmitted value, but
        /// the final result is NOT guaranteed to match one of those formats.
        /// </summary>
        /// <param name="year"></param>
        /// <returns>The transformed value for Year</returns>
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

        /// <summary>
        /// Verify that the specified Year value conforms to one of the following forms:
        ///     YYYY
        ///     YYYY-YYYY
        ///     YYYY,YYYY
        /// </summary>
        /// <param name="year"></param>
        /// <returns>True if the year is in the correct form, otherwise False.</returns>
        public static bool ValidateItemYear(string year)
        {
            bool isValid = true;

            if (!string.IsNullOrWhiteSpace(year))
            {
                // YYYY
                Regex regex = new Regex("^[0-9]{4}$");
                Match match = regex.Match(year);
                isValid = match.Success;

                if (!isValid)
                {
                    // YYYY-YYYY
                    regex = new Regex("^[0-9]{4}-[0-9]{4}$");
                    match = regex.Match(year);
                    isValid = match.Success;
                }

                if (!isValid)
                {
                    // YYYY,YYYY
                    regex = new Regex("^[0-9]{4},[0-9]{4}$");
                    match = regex.Match(year);
                    isValid = match.Success;
                }
            }

            return isValid;
        }

        /// <summary>
        /// Break the specified volume string into its component parts.  For example,
        /// consider the volume string "v.14 no.1-2 (1900-1901)".  This method will
        /// return an object that includes the following values:
        ///     Volume = "v.14 no.1-2 (1900-1901)"
        ///     StartVolume = "14"
        ///     StartNumber = "1"
        ///     EndNumber = "2"
        ///     StartYear = "1900"
        ///     EndYear = "1901"
        /// </summary>
        /// <param name="volume">The volume string to be parsed</param>
        /// <returns>An object containing the individual elements of the volume string</returns>
        public static VolumeData ParseVolumeString(string volume)
        {
            /*
            GERMAN
            Abt. (Abteilung)	= Part
            Bd. (Band)			= Volume
            Heft/Hft			= Number
            Jahrg				= Volume
            Lf. (Lieferung)		= Edition, Series (?)
            Th. (Teil?)			= Part
            F. (Folge)			= Series

            DUTCH
            d. (Deel)			= Part
            reeks				= Series

            FRENCH
            ptie (Partie)		= Part
            Tome				= Volume
            Yr./Année			= Volume (ex. Année 1)

            RUSSIAN
            Ch.                 = Part
            vyp.				= Issue/Number

            MISC
            fascicle			= Part
            Section				= Part
            Session				= Part
            Suppl./Supplement	= In most cases, probably doesn't map to a field
            */
            bool found = false;

            VolumeData volumeData = new VolumeData();
            volumeData.Volume = volume;
            // Do initial clean-up of volume to be parsed
            volumeData.VolumeParsed = CleanVolumeParsed(volume, " ");

            // 00 0000-0000
            // 00 0000 - 0000
            // 00, 0000-0000
            // 00 (0000-0000)
            // 0-0, 0000-0000
            // 00-000, 0000-0000
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^([0-9]{1,3})( ??[-|/|:] ??([0-9]{1,3}))??,?? \\(??([0-9]{4}) ??[-|/|:] ??([0-9]{4})\\)??", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[1].Value;
                    volumeData.EndVolume = groups[3].Value;
                    volumeData.StartYear = groups[4].Value;
                    volumeData.EndYear = groups[5].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            // 00 0000
            // 00, 0000
            // 00 (0000)
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^([0-9]+),?? \\(??([0-9]{4})\\)??", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[1].Value;
                    volumeData.StartYear = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                // v.00-00 0000-0000
                // v.0-00 0000-0000
                // v.0-0 0000-0000
                // v.00-00 (0000-0000)
                // v.0-00 (0000-0000)
                // v.0-0 (0000-0000)
                // jahrg 00-00 0000-0000
                // v 0 0000
                // v 0-0 (0000-0000)
                GroupCollection groups = GetMatchGroups("^(v|t|bd|jahrg)\\.?? ??([0-9]+) ??[-|/]?? ??([0-9]+)?? ??\\(??([0-9]{4}) ??[-|/|:]?? ??([0-9]{4})??\\)??$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[2].Value;
                    volumeData.EndVolume = groups[3].Value;
                    volumeData.StartYear = groups[4].Value;
                    volumeData.EndYear = groups[5].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                // v.0 (0000)
                // v.00 (0000)
                GroupCollection groups = GetMatchGroups("^(v|t|bd|jahrg)\\.?? ??([0-9]+) \\(([0-9]{4})\\)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[2].Value;
                    volumeData.StartYear = groups[3].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                // heft.00-00 0000-0000
                // heft.0-00 0000-0000
                // heft.0-0 0000-0000
                // heft.00-00 (0000-0000)
                // heft.0-00 (0000-0000)
                // heft.0-0 (0000-0000)
                GroupCollection groups = GetMatchGroups("^heft\\.?? ??([0-9]+) ??[-|/]?? ??([0-9]+)?? ??\\(??([0-9]{4}) ??[-|/|:]?? ??([0-9]{4})??\\)??$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartNumber = groups[1].Value;
                    volumeData.EndNumber = groups[2].Value;
                    volumeData.StartYear = groups[3].Value;
                    volumeData.EndYear = groups[4].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                volumeData = GetPartsFromVolumeString(volumeData);
                volumeData = GetNumbersFromVolumeString(volumeData);
                volumeData = GetVolumesFromVolumeString(volumeData);
                volumeData = GetSeriesFromVolumeString(volumeData);
                volumeData = GetIssuesFromVolumeString(volumeData);
                volumeData = GetYearsFromVolumeString(volumeData);

                // If still no volume identified, see if there are numeric values left over at the
                // start of the volume string
                if (string.IsNullOrEmpty(volumeData.StartVolume))
                {
                    // 0-0, some other text
                    // 0/0, some other text
                    if (!found)
                    {
                        GroupCollection groups = GetMatchGroups("^([0-9]+) ??[-|/] ??([0-9]+).*", volumeData.VolumeParsed);
                        if (groups != null)
                        {
                            volumeData.StartVolume = groups[1].Value;
                            volumeData.EndVolume = groups[2].Value;
                            found = true;
                        }
                    }

                    // 00 some other text
                    if (!found)
                    {
                        GroupCollection groups = GetMatchGroups("^([0-9]+).*", volumeData.VolumeParsed);
                        if (groups != null)
                        {
                            volumeData.StartVolume = groups[1].Value;
                            found = true;
                        }
                    }
                }
            }

            return volumeData;
        }

        /// <summary>
        /// Extract the start and end Volume values from the volume string
        /// </summary>
        /// <param name="volumeData"></param>
        /// <returns></returns>
        private static VolumeData GetVolumesFromVolumeString(VolumeData volumeData)
        {
            bool found = false;

            // 00000
            // 000
            // 00
            // 0
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^([0-9]{1,3}|[0-9]{5,})$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[0].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            // 0:0
            // 0/0
            // 0-0
            // 00-00
            // 000-000
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^([0-9]{1,3}) ??[-|:|/] ??([0-9]{1,3})$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[1].Value;
                    volumeData.EndVolume = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            // v. IV
            // v. 0
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^(vol|volume|v)\\.?? ??([^ -.]*)$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            // Band II
            // bd 0
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^(band|bd)\\.?? ??([^ -.]*)$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                // v.0
                // vol.0
                GroupCollection groups = GetMatchGroups("^(v|t|vol)\\.?? ??([0-9]+)$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                // Tom 0 - Tom 0
                // Tom. 00 - Tom. 00
                // Tome 0 - Tome 0
                // Tome 0
                // Tom. 000
                GroupCollection groups = GetMatchGroups("^Tom[e|o|\\.]?? ([0-9]+)( ??- ??Tom[e|o|\\.]?? ([0-9]+))??$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[1].Value;
                    volumeData.EndVolume = groups[3].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                // Tom. II
                // Tomo VIIII
                GroupCollection groups = GetMatchGroups("^Tom[e|o|\\.]?? ([^ -.]*)$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[1].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("vol\\.?? ??([0-9]+) ??[-|/] ??(vol\\.?? ??)??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[1].Value;
                    volumeData.EndVolume = groups[3].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("vol\\.?? ??([0-9]+)", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartVolume = groups[1].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                // The (^|[^a-zA-Z]) portion of the regular expression ensures that things
                // like 'Nov', 'Sept', ect, are not mistaken for volume designations
                GroupCollection groups = GetMatchGroups("(^|[^a-zA-Z])(t|v|jahrg??|bd|haft|hft|kot|ct|bericht)\\.?? ??([0-9]+) ??[-|/] ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[3].Value;
                    volumeData.EndVolume = groups[4].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("(^|[^a-zA-Z])(t|v|jahrg??|bd|haft|hft|kot|ct|bericht)\\.?? ??([0-9]+)", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartVolume = groups[3].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("d\\. ??([0-9]+) ??[-|/] ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[1].Value;
                    volumeData.EndVolume = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("d\\. ??([0-9]+)", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartVolume = groups[1].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("(band|bd)\\.?? ??([0-9]+) ??[-|/] ??(band|bd)\\.?? ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[2].Value;
                    volumeData.EndVolume = groups[4].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("tom[e|o|\\.]??\\.?? ??([0-9]+) ??[-|/] ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[1].Value;
                    volumeData.EndVolume = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("tom[e|o|\\.]??\\.?? ??([0-9]+)", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartVolume = groups[1].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            // Tomus 0
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("tomus ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[1].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            // Just Roman numerals (unvalidated)
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^([MDCLXVI]+)$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartVolume = groups[1].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            return volumeData;
        }

        /// <summary>
        /// Extract the start and end Year values from the volume string
        /// </summary>
        /// <param name="volumeData"></param>
        /// <returns></returns>
        private static VolumeData GetYearsFromVolumeString(VolumeData volumeData)
        {
            bool found = false;

            // 0000-0000
            // 0000 - 0000
            // 0000/0000
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^([0-9]{4}) ??(-|/) ??([0-9]{4})$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value) && IsValidYear(groups[3].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.EndYear = groups[3].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            // 0000-00
            // 0000 - 00
            // 0000/00
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^([0-9]{4}) ??[-|/] ??([0-9]{2})$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value) && IsValidYear(groups[1].Value.Substring(0, 2) + groups[2].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.EndYear = groups[1].Value.Substring(0, 2) + groups[2].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            // 0000-0
            // 0000 - 0
            // 0000/0
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^([0-9]{4}) ??[-|/] ??([0-9]{1})$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value) && IsValidYear(groups[1].Value.Substring(0, 3) + groups[2].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.EndYear = groups[1].Value.Substring(0, 3) + groups[2].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            // Starts with:
            // 0000-0000
            // (0000-0000)
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^\\(??([0-9]{4}) ??(-|/) ??([0-9]{4})\\)??", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value) && IsValidYear(groups[3].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.EndYear = groups[3].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("\\(([0-9]{4})(\\)|:)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("\\(([0-9]{4}) ??[-|/] ??([0-9]{4})\\)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value) && IsValidYear(groups[2].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.EndYear = groups[2].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("([0-9]{4}) ??[-|/] ??([0-9]{2})([^0-9]|$)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value) && IsValidYear(groups[1].Value.Substring(0, 2) + groups[2].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.EndYear = groups[1].Value.Substring(0, 2) + groups[2].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            // Starts with:
            // 0000
            // (0000)
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^\\(??\\b([0-9]{4})\\b\\)??", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            // 0000 January
            // 0000:winter
            // 0000 (july)
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^([0-9]{4})( |:)\\(??[a-z]+", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            // Senders Be-Bo, 0000-0000
            // Shaffer to Engelmann, 0000
            // Short to Engelmann, 0000-0000
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^.*[a-z]+.*, ??([0-9]{4}) ??[-|/] ??([0-9]{4})$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value) && IsValidYear(groups[2].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.EndYear = groups[2].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
                else
                {
                    groups = GetMatchGroups("^.*[a-z]+.*, ??([0-9]{4})$", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        if (IsValidYear(groups[1].Value))
                        {
                            volumeData.StartYear = groups[1].Value;
                            volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                            found = true;
                        }
                    }
                }
            }

            if (!found)
            {
                // 0000-0000 anywhere within string (if dates not already parsed by a previous rule)
                GroupCollection groups = GetMatchGroups("([0-9]{4}) ??(-|/) ??([0-9]{4})", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value) && IsValidYear(groups[3].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.EndYear = groups[3].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                // 0000 anywhere within string (if dates not already parsed by a previous rule)
                GroupCollection groups = GetMatchGroups("\\b([0-9]{4})\\b", volumeData.VolumeParsed);
                if (groups != null)
                {
                    if (IsValidYear(groups[1].Value))
                    {
                        volumeData.StartYear = groups[1].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            return volumeData;
        }

        /// <summary>
        /// Make sure the specified publication year is not greater than the current year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private static bool IsValidYear(string year)
        {
            if (!string.IsNullOrWhiteSpace(year))
            {
                int currentYear = DateTime.Now.Year;
                if (Convert.ToInt32(year) > currentYear) return false;
            }
            return true;
        }

        /// <summary>
        /// Extract the start and end Series values from the volume string
        /// </summary>
        /// <param name="volumeData"></param>
        /// <returns></returns>
        private static VolumeData GetSeriesFromVolumeString(VolumeData volumeData)
        {
            bool found = false;

            // new ser.
            // new ser
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("(new) ser\\.??", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartSeries = groups[1].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            // 0st ser.
            // 0nd ser.
            // 0rd ser.
            // 0th ser.
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("([0-9]+)(st|nd|rd|th) ser\\.??", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartSeries = groups[1].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("(ser|series)\\.?? ??([0-9]+) ??[-|/|:] ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartSeries = groups[2].Value;
                    volumeData.EndSeries = groups[3].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("(ser|series)\\.?? ??([0-9]+)", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartSeries = groups[2].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            return volumeData;
        }

        /// <summary>
        /// Extract the start and end Part values from the volume string
        /// </summary>
        /// <param name="volumeData"></param>
        /// <returns></returns>
        private static VolumeData GetPartsFromVolumeString(VolumeData volumeData)
        {
            bool found = false;

            // pt. IV
            // pt 0
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^(pt|pars|part).?? ??([^ -.]*)$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartPart = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                // Remove 'sept' from string to evaluate to prevent false positives
                string volumeParsed = volumeData.VolumeParsed.Replace("Sept", "").Replace("sept", "");

                GroupCollection groups = GetMatchGroups("(pt|pts|fasc|fascicle|pars|parts|fuzet)\\.?? ??([0-9]+) ??(-|\\.|/) ??([0-9]+)", volumeParsed);
                if (groups != null)
                {
                    volumeData.StartPart = groups[2].Value;
                    volumeData.EndPart = groups[4].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("(pt|fasc|fascicle|fuzet)\\.?? ??([0-9]+)", volumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartPart = groups[2].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("ptie\\.?? ??([0-9]+) ??[-|/] ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartPart = groups[1].Value;
                    volumeData.EndPart = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("ptie\\.?? ??([0-9]+)", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartPart = groups[1].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("abt\\.?? ??([0-9]+) ??[-|/] ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartPart = groups[1].Value;
                    volumeData.EndPart = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("abt\\.?? ??([0-9]+)", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartPart = groups[1].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("part ??([0-9]+) ??[-|/] ??part ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartPart = groups[1].Value;
                    volumeData.EndPart = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("part ??([0-9]+)", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartPart = groups[1].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            return volumeData;
        }

        /// <summary>
        /// Extract the start and end Number values from the volume string
        /// </summary>
        /// <param name="volumeData"></param>
        /// <returns></returns>
        private static VolumeData GetNumbersFromVolumeString(VolumeData volumeData)
        {
            bool found = false;

            // no.000
            // no. 000A
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("^(no|heft)\\.?? ??([0-9]+)([a-zA-Z])??$", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartNumber = groups[2].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
            }

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("(no|heft|nos|num|number|nr|vyp)\\.?? ??\\[??([0-9]+) ??[-|/] ??([0-9]+)\\]??", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartNumber = groups[2].Value;
                    volumeData.EndNumber = groups[3].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("(no|heft|nos|num|number|nr|vyp)\\.?? ??\\[??([0-9]+)\\]??", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartNumber = groups[2].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            // Find instances of n.00 and n.00-00, but NOT Jan.00, Jul.00, Division 0, and so on.
            if (!found)
            {
                GroupCollection groups = GetMatchGroups("([^a-zA-Z]|^)n\\.?? ??([0-9]+) ??[-|/] ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartNumber = groups[2].Value;
                    volumeData.EndNumber = groups[3].Value;
                    string match = groups[0].Value.Substring(0, 1).ToLower() == "n" ? groups[0].Value : groups[0].Value.Substring(1);
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, match);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("([^a-zA-Z]|^)n\\.?? ??([0-9]+)", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartNumber = groups[2].Value;
                        string match = groups[0].Value.Substring(0, 1).ToLower() == "n" ? groups[0].Value : groups[0].Value.Substring(1);
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, match);
                        found = true;
                    }
                }
            }

            return volumeData;
        }

        /// <summary>
        /// Extract the start and end Issue values from the volume string
        /// </summary>
        /// <param name="volumeData"></param>
        /// <returns></returns>
        private static VolumeData GetIssuesFromVolumeString(VolumeData volumeData)
        {
            bool found = false;

            if (!found)
            {
                GroupCollection groups = GetMatchGroups("(iss\\.|issue) ??([0-9]+) ??[-|/] ??([0-9]+)", volumeData.VolumeParsed);
                if (groups != null)
                {
                    volumeData.StartIssue = groups[2].Value;
                    volumeData.EndIssue = groups[3].Value;
                    volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                    found = true;
                }
                else
                {
                    groups = GetMatchGroups("(iss\\.|issue) ??([0-9]+)", volumeData.VolumeParsed);
                    if (groups != null)
                    {
                        volumeData.StartIssue = groups[2].Value;
                        volumeData.VolumeParsed = CleanVolumeParsed(volumeData.VolumeParsed, groups[0].Value);
                        found = true;
                    }
                }
            }

            return volumeData;
        }

        /// <summary>
        /// Remove the elements of the volume that have already been identified
        /// </summary>
        /// <param name="volumeParsed"></param>
        /// <param name="stringToRemove"></param>
        /// <returns></returns>
        private static string CleanVolumeParsed(string volumeParsed, string stringToRemove)
        {
            // Remove the already-identified part of the volume
            volumeParsed = volumeParsed.Replace(stringToRemove, " ").Trim();

            // Clean up duplicated separator characters
            volumeParsed = volumeParsed.Replace("::", ":");
            volumeParsed = volumeParsed.Replace(";;", ";");
            volumeParsed = volumeParsed.Replace(",,", ",");
            volumeParsed = volumeParsed.Replace("..", ".");

            // Clean up trailing punctuation
            if (volumeParsed.EndsWith(":")) volumeParsed = volumeParsed.Substring(0, volumeParsed.Length - 1);
            if (volumeParsed.EndsWith(";")) volumeParsed = volumeParsed.Substring(0, volumeParsed.Length - 1);
            if (volumeParsed.EndsWith(",")) volumeParsed = volumeParsed.Substring(0, volumeParsed.Length - 1);
            if (volumeParsed.EndsWith(".")) volumeParsed = volumeParsed.Substring(0, volumeParsed.Length - 1);
            if (volumeParsed.EndsWith("/")) volumeParsed = volumeParsed.Substring(0, volumeParsed.Length - 1);
            if (volumeParsed.EndsWith("=")) volumeParsed = volumeParsed.Substring(0, volumeParsed.Length - 1);

            // Clean up leading punctuation
            if (volumeParsed.StartsWith(":")) volumeParsed = volumeParsed.Substring(1);
            if (volumeParsed.StartsWith(";")) volumeParsed = volumeParsed.Substring(1);
            if (volumeParsed.StartsWith(",")) volumeParsed = volumeParsed.Substring(1);
            if (volumeParsed.StartsWith(".")) volumeParsed = volumeParsed.Substring(1);
            if (volumeParsed.StartsWith("/")) volumeParsed = volumeParsed.Substring(1);
            if (volumeParsed.StartsWith("=")) volumeParsed = volumeParsed.Substring(1);

            return volumeParsed;
        }

        private static GroupCollection GetMatchGroups(string pattern, string volume)
        {
            GroupCollection groups = null;

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(volume);
            if (match.Success) groups = match.Groups;

            return groups;
        }
    }

    /// <summary>
    /// Data structure to contain individual elements of a parsed volume string
    /// </summary>
    public class VolumeData
    {
        public VolumeData()
        {
            Volume = string.Empty;
            StartYear = string.Empty;
            EndYear = string.Empty;
            StartVolume = string.Empty;
            EndVolume = string.Empty;
            StartIssue = string.Empty;
            EndIssue = string.Empty;
            StartNumber = string.Empty;
            EndNumber = string.Empty;
            StartSeries = string.Empty;
            EndSeries = string.Empty;
            StartPart = string.Empty;
            EndPart = string.Empty;
        }

        public string Volume { get; set; }  // The unparsed, unmodified volume string

        // Volume string modified during parsing
        private string _volumeParsed = string.Empty;
        public string VolumeParsed
        {
            get { return _volumeParsed; }
            set { _volumeParsed = value.Replace("  ", " "); } // Replace double spaces with single spaces
        }

        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string StartVolume { get; set; }
        public string EndVolume { get; set; }
        public string StartIssue { get; set; }
        public string EndIssue { get; set; }
        public string StartNumber { get; set; }
        public string EndNumber { get; set; }
        public string StartSeries { get; set; }
        public string EndSeries { get; set; }
        public string StartPart { get; set; }
        public string EndPart { get; set; }
    }
}
