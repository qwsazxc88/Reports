using System.Globalization;

namespace Reports.Core
{
    public static class StringHelper
    {
        public static string Format (string formatPar, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, formatPar, args);
        }
    }
}
