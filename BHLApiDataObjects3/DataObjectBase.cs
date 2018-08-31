using System;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    public class DataObjectBase
    {
        protected string CalibrateValue(string value, int maximumCharacterLength)
        {
            value = value.Trim();
            if (value.Length > maximumCharacterLength)
            {
                value = value.Substring(0, maximumCharacterLength);
            }

            return value;
        }
    }
}
