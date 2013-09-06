using System;

namespace BHLApi3Web.Models
{
    public class DataObjectBase
    {
        protected string CalibrateValue(string value, int maximumCharacterLength)
        {
            if (value != null)
            {
                value = value.Trim();
                if (value.Length > maximumCharacterLength)
                {
                    value = value.Substring(0, maximumCharacterLength);
                }
            }

            return value;
        }
    }
}
