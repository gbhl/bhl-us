using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.Server
{
    public class RomanNumerals
    {
        //Heavily adapted from post by Simon Trew on 3/24/2003 (had numerous errors)
        //http://groups.google.com/group/microsoft.public.dotnet.languages.csharp/browse_thread/thread/da9495233b0fce93/da321010fd7fd92f?lnk=st&q=csharp+Simon+Trew+roman&rnum=2&hl=en#da321010fd7fd92f

        private struct Number
        {
            int _value; string _rep;
            internal Number(int value, string rep) { _value = value; _rep = rep; }
            public int Value { get { return _value; } }
            public string Rep { get { return _rep; } }

        }

        private static Number[] numbers;

        private static void initializeNumbers()
        {
            numbers = new Number[13]
                {
                    new Number(1000, "M"),
                    new Number(900, "CM"),
                    new Number(500, "D"),
                    new Number(400, "CD"),
                    new Number(100, "C"),
                    new Number(90, "XC"),
                    new Number(50, "L"),
                    new Number(40, "XL"),
                    new Number(10, "X"),
                    new Number(9, "IX"),
                    new Number(5, "V"),
                    new Number(4, "IV"),
                    new Number(1, "I"),
                };
        }

        public static string ToRomanNumeral(int number)
        {
            return ToRomanNumeral(number, true);
        }

        public static string ToRomanNumeral(int number, bool useIV)
        {
            initializeNumbers();
            
            // Should check first for number in range... omitted for clarity.
            StringBuilder result = new StringBuilder();
            int numIndex = 0;
            int val = 0;
            while (number > 0)
            {
                val = numbers[numIndex].Value;
                if (number >= val)
                {
                    number -= val;
                    result.Append(numbers[numIndex].Rep);
                }
                else
                {
                    numIndex++;
                    // if not using IV, skip value for IV
                    if (!useIV && (numIndex == 11)) numIndex++;
                }
            }
            return result.ToString();

        }

        public static int FromRomanNumeral(string roman)
        {
            initializeNumbers();
            int result = 0;
            string remaining = roman.ToUpper();
            int incremental = 0;

            while (remaining.Length > 0)
            {
                incremental = valueFromString(ref remaining);

                // avoid infinite loop
                if (incremental == 0) break;

                // otherwise, add found value
                result = result + incremental;
            }

            return result;
        }

        private static int valueFromString(ref string roman)
        {
            int value = 0;

            value = lookForMatch(2, ref roman);
            if (value == 0)
            {
                value = lookForMatch(1, ref roman);
            }

            return value;

        }

        private static int lookForMatch(int numChars, ref string roman)
        {
            int value = 0;
            string toCompare = "";

            if (roman.Length >= numChars)
            {
                toCompare = roman.Substring(0, numChars);
            }
            else
            {
                return 0;
            }

            for (int index = 0; index < numbers.Length; index++)
            {
                if (numbers[index].Rep == toCompare)
                {
                    value = numbers[index].Value;

                    if (roman.Length == numChars) roman = "";
                    else roman = roman.Substring(numChars);

                    return value;
                }
            }

            return value;
        }
    }
}
