using System;
using System.Text.RegularExpressions;

namespace MOBOT.BHL.DOIDeposit
{
    public class DOIDate
    {
        private string _dateString = string.Empty;
        private string _month = null;
        private string _day = null;
        private string _year = null;
        private string _error = string.Empty;

        public string DateString { get => _dateString; set => _dateString = value; }
        public string Month { get => _month; }
        public string Day { get => _day; }
        public string Year { get => _year; }

        public DOIDate()
        {

        }

        public DOIDate(string dateString)
        {
            DateString = dateString;
            Parse(DateString);
        }

        public bool Parse(string dateString)
        {
            bool isValid = false;

            // YYYY
            GroupCollection groups = GetMatchGroups("^([0-9]{4})$", dateString);
            if (groups != null)
            {
                _year = groups[1].Value;
                isValid = true;
            }

            if (!isValid)
            {
                // YYYY-YYYY
                groups = GetMatchGroups("^([0-9]{4})-([0-9]{4})$", dateString);
                if (groups != null)
                {
                    _year = groups[1].Value;
                    isValid = true;
                }
            }

            if (!isValid)
            {
                // YYYY-MM
                groups = GetMatchGroups("^([0-9]{4})-([0-9]{2})$", dateString);
                if (groups != null)
                {
                    _year = groups[1].Value;
                    _month = groups[2].Value;
                    isValid = true;
                }
            }

            if (!isValid)
            {
                // YYYY-MM-DD
                groups = GetMatchGroups("^([0-9]{4})-([0-9]{2})-([0-9]{2})$", dateString);
                if (groups != null)
                {
                    _year = groups[1].Value;
                    if (Convert.ToInt32(groups[2].Value) > 0) _month = groups[2].Value;
                    if (Convert.ToInt32(groups[3].Value) > 0) _day = groups[3].Value;
                    isValid = true;
                }
            }

            return isValid;
        }

        private static GroupCollection GetMatchGroups(string pattern, string date)
        {
            GroupCollection groups = null;

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(date);
            if (match.Success) groups = match.Groups;

            return groups;
        }
    }
}
