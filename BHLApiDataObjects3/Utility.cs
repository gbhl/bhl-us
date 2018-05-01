namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    public class Utility
    {
        public static string EmptyIfNull(object value)
        {
            if (value == null)
            {
                return "";
            }
            return (string)value;
        }

        public static int ZeroIfNull(object value)
        {
            if (value == null)
            {
                return 0;
            }
            return (int)value;
        }

        public static bool FalseIfNull(object value)
        {
            if (value == null)
            {
                return false;
            }
            return (bool)value;
        }
    }
}
