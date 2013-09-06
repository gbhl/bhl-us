using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    public class OpenUrlQueryDate
    {
        #region Properties

        string _date = string.Empty;

        #endregion

        #region Constructors

        public OpenUrlQueryDate()
        {
        }

        public OpenUrlQueryDate(string date)
        {
            _date = date.Trim();
        }

        #endregion Constructors

        #region Methods

        public override string ToString()
        {
            string message = string.Empty;
            return (IsValid(out message) ? _date : "");
        }

        public string Year()
        {
            if (_date == string.Empty)
            {
                return _date;
            }
            else
            {
                string message = string.Empty;
                return (IsValid(out message) ? _date.Substring(0, 4) : "");
            }
        }

        public string Month()
        {
            if (_date == string.Empty)
            {
                return _date;
            }
            else
            {
                string message = string.Empty;
                string month = string.Empty;
                if (IsValid(out message))
                {
                    if (_date.Length >= 7) month = _date.Substring(5, 2);
                }
                return month;
            }
        }

        public string Day()
        {
            if (_date == string.Empty)
            {
                return _date;
            }
            else
            {
                string message = string.Empty;
                string day = string.Empty;
                if (IsValid(out message))
                {
                    if (_date.Length == 10) day = _date.Substring(8, 2);
                }
                return day;
            }
        }

        public bool IsValid(out string validationMessage)
        {
            bool isValid = true;
            validationMessage = string.Empty;

            if (this._date != string.Empty)
            {
                // valid date formats are YYYY-MM-DD, YYYY-MM, and YYYY
                if (!System.Text.RegularExpressions.Regex.IsMatch(this._date, "^((((10|11|12|13|14|15|16|17|18|19|20)(([02468][048])|([13579][26]))-02-29))|((20[0-9][0-9])|(19[0-9][0-9])|(18[0-9][0-9])|(17[0-9][0-9])|(16[0-9][0-9])|(15[0-9][0-9])|(14[0-9][0-9])|(13[0-9][0-9])|(12[0-9][0-9])|(11[0-9][0-9])|(10[0-9][0-9]))-((((0[1-9])|(1[0-2]))-((0[1-9])|(1\\d)|(2[0-8])))|((((0[13578])|(1[02]))-31)|(((0[1,3-9])|(1[0-2]))-(29|30)))))$") &&
                    !System.Text.RegularExpressions.Regex.IsMatch(this._date, "^((((10|11|12|13|14|15|16|17|18|19|20)(([02468][048])|([13579][26]))-02-29))|((20[0-9][0-9])|(19[0-9][0-9])|(18[0-9][0-9])|(17[0-9][0-9])|(16[0-9][0-9])|(15[0-9][0-9])|(14[0-9][0-9])|(13[0-9][0-9])|(12[0-9][0-9])|(11[0-9][0-9])|(10[0-9][0-9]))-((((0[1-9])|(1[0-2])))))$") &&
                    !System.Text.RegularExpressions.Regex.IsMatch(this._date, "^((((10|11|12|13|14|15|16|17|18|19|20)(([02468][048])|([13579][26]))-02-29))|((20[0-9][0-9])|(19[0-9][0-9])|(18[0-9][0-9])|(17[0-9][0-9])|(16[0-9][0-9])|(15[0-9][0-9])|(14[0-9][0-9])|(13[0-9][0-9])|(12[0-9][0-9])|(11[0-9][0-9])|(10[0-9][0-9])))$"))
                {
                    validationMessage = "Invalid Date format (use YYYY-MM-DD, YYYY-MM, or YYYY)";
                    isValid = false;
                }
            }

            return isValid;
        }

        #endregion

    }
}
