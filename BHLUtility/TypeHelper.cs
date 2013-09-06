using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MOBOT.BHL.Utility
{
    public class TypeHelper
    {
        #region Date Data

        private static string[] _monthList = {
		"01","02","03","04","05","06","07","08","09","1","10","11","12","2","3","4","5","6","7","8","9","abr","abril",
		"Ago","Agosto","Agosto","Aout","Août","Apr","April","Aprilis","Aug","August","Augustus","Avr","Avril","Dec","Déc",
		"December","Decembre","Décembre","Dez","Dez","Dezcembre","Dezembro","Dic","Diciembre","Ene","Enero","Feb","Febrero",
		"Februar","Februarius","February","Fev","Fév","Fevereiro","Fevrier","Février","I","II","III","IV","IX","Jan",
		"Janeiro","Januar","Januarius","January","Janvier","Juil","Juillet","Juin","Jul","Julho","Juli","Julio","Julius",
		"July","Jun","June","Junho","Juni","Junio","Junius","Mai","Mai","Maio","Maius","Mar","March","Marco","Março","Maritus","Mars",
		"Marz","März","Marzo","May","Mayo","Nov","November","Novembre","Novembris","Novembro","Noviembre","Oct","October","Octobre",
		"Octubre","Okt","Oktober","Out","Outubro","Sep","Sept","September","Septembre","Septiembre","Set","Setembro","Setiembre","V",
		"VI","VII","VIII","X","XI","XII"};

        private static int[] _monthNumberList = {
		1,2,3,4,5,6,7,8,9,1,10,11,12,2,3,4,5,6,7,8,9,4,4,8,8,8,8,8,4,4,4,8,8,8,4,4,12,12,12,12,12,
		12,12,12,12,12,12,1,1,2,2,2,2,2,2,2,2,2,2,1,2,3,4,9,1,1,1,1,1,1,6,7,6,7,7,7,7,7,7,6,6,6,
		6,6,6,5,5,5,5,3,3,3,3,3,3,3,3,3,5,5,11,11,11,11,11,11,10,10,10,10,10,10,10,10,9,9,9,9,9,9,
		9,9,5,6,7,8,10,11,12};

        #endregion

        private static Regex _numericRegEx;

        static TypeHelper()
        {
            _numericRegEx = new Regex(@"^-?(?<number>[0-9]+(\.)?([0-9]+)?)$");
        }

        public static bool IsUrlValid(string url)
        {
            if (url == null || url.Trim().Length == 0)
            {
                return false;
            }
            else
            {
                Regex regex = new Regex(@"^(?<Protocol>\w+):\/\/(?<Domain>[\w.]+\/?)\S*(?x)");
                Match match = regex.Match(url.Trim());

                return match.Success;
            }
        }

        public static string GetUrlTLD(string url)
        {
            if (string.IsNullOrEmpty(url) == true)
            {
                return "";
            }
            else
            {
                Regex regex = new Regex(@"(http:\/\/[\w@][\w.:@]+\/?)[\w\.?=%&=\-@/$,]*");
                Match match = regex.Match(url.Trim());

                if (match.Success && match.Groups.Count > 1)
                {
                    return match.Groups[1].Value;
                }
                else
                {
                    return "";
                }
            }
        }

        public static bool IsEmailAddressValid(string address)
        {
            const string expression = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
            Regex regex = new Regex(expression);
            if (regex.IsMatch(address))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsBoolean(string val)
        {
            if (val == null)
            {
                return false;
            }

            if (val.Trim().Equals("1") || val.Trim().Equals("0") || val.ToLower().Trim().Equals("true") ||
                val.ToLower().Trim().Equals("false"))
            {
                return true;
            }

            return false;
        }

        public static bool IsNumeric(string val)
        {
            if (val == null)
            {
                return false;
            }
            Match match = _numericRegEx.Match(val.Trim());
            return match.Success;
        }

        public static bool IsValidDate(int? year, int? month, int? day)
        {
            if (day.HasValue && month.HasValue && year.HasValue)
            {
                try
                {
                    DateTime test = new DateTime(year.Value, month.Value, day.Value);

                    if (test >= DateTime.MinValue && test <= DateTime.MaxValue)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDate(string day, string month, string year)
        {
            if (day == null || month == null || year == null)
            {
                return false;
            }

            try
            {
                DateTime test = new DateTime(int.Parse(year), GetNumericMonth(month), int.Parse(day));
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool GetBoolenValue(string val)
        {
            if (val == null)
            {
                throw new ArgumentNullException(val);
            }
            else if (val.Trim().Equals("1") || val.ToLower().Trim().Equals("true"))
            {
                return true;
            }
            else if (val.Trim().Equals("0") || val.ToLower().Trim().Equals("false"))
            {
                return false;
            }
            else
            {
                throw new ArgumentException("Value does not equal 0,1,true or false");
            }
        }

        public static int GetNumericMonth(string monthText)
        {
            for (int i = 0; i < _monthList.Length; i++)
            {
                if (_monthList[i] == monthText)
                {
                    return (_monthNumberList[i]);
                }
            }
            return 0;
        }

        public static int? GetYear(int? nyear)
        {
            int? year = new Int32?();

            if (nyear.HasValue && nyear.Value > 0)
            {
                string y = nyear.ToString();
                if (y.Length == 3)
                {
                    y = y.Replace("'", "");
                }
                if (y.Length == 2)
                {
                    y = "19" + y;
                }

                try
                {
                    year = Int32.Parse(y);
                }
                catch
                {
                    return year;
                }
            }

            return year;
        }

        public static bool AreEqual(string val1, string val2)
        {
            string compare1 = (val1 == null ? "" : val1);
            string compare2 = (val2 == null ? "" : val2);
            if (compare1 != compare2)
            {
                return false;
            }
            return true;
        }

        public static bool AreEqual(int? val1, int? val2)
        {
            if (!val1.HasValue && !val2.HasValue)
            {
                return true; // both are null
            }

            if (val1.HasValue && val2.HasValue)
            {
                return (val1.Value == val2.Value);
            }
            else
            {
                return false; // one is null and the other isn't
            }
        }

        public static string EmptyIfNull(object value)
        {
            if (value == null)
            {
                return "";
            }

            return value.ToString();
        }

        public static DateTime MinDateIfNull(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static string NullIfEmpty(object value)
        {
            if (value == null || ((string)value).Trim().Length == 0)
            {
                return null;
            }
            return ((string)value).Trim();
        }

        public static int ZeroIfNull(object value)
        {
            if (value == null)
            {
                return 0;
            }
            return (int)value;
        }

        public static int? NullIfZero(object value)
        {
            if (value == null || ((int)value) == 0)
            {
                return null;
            }
            return (int)value;
        }

        public static int IntParse(string intString)
        {
            //input string not in the correct format error
            //http://www.eggheadcafe.com/software/aspnet/29435858/very-strange-input-strin.aspx
            return int.Parse(intString, NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        public static bool IntTryParse(string intString, out int returnInt)
        {
            return int.TryParse(intString, NumberStyles.Any, CultureInfo.InvariantCulture, out returnInt);
        }

        public static decimal DecimalParse(decimal d, int totalDigits, int fracDigits)
        {
            if (totalDigits > fracDigits && totalDigits > 0)
            {
                string fracMask = new String('0', fracDigits);
                string mask = "0." + fracMask;
                string ds = d.ToString(mask);
                if (ds.Length > totalDigits + 1)
                {
                    ds = ds.Substring(ds.Length - (totalDigits + 1));
                }

                return decimal.Parse(ds);
            }
            else
            {
                return d;
            }
        }

        public static string DecimalToString(decimal d, int totalDigits, int fracDigits)
        {
            if (totalDigits > fracDigits && totalDigits > 0)
            {
                string fracMask = new String('0', fracDigits);
                string mask = "0." + fracMask;
                string ds = d.ToString(mask);
                if (ds.Length > totalDigits + 1)
                {
                    ds = ds.Substring(ds.Length - (totalDigits + 1));
                }

                return ds;
            }
            else
            {
                return d.ToString();
            }
        }

        /// <summary>
        /// Milliseconds since 1/1/1970
        /// </summary>
        /// <returns></returns>
        public static double GetEpochTime()
        {
            return GetEpochTime(DateTime.UtcNow);
        }

        /// <summary>
        /// Milliseconds from UTC date to 1/1/1970
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static double GetEpochTime(DateTime dt)
        {
            return (dt - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}
