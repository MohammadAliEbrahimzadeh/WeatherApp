using System.Text.RegularExpressions;

namespace WeatherApp.Core.Helpers;

public static class RegexHelper
{
    public static readonly Regex IsPureEnglishRegex =
        new Regex(@"^[\u0000-\u007F]*$", RegexOptions.Compiled);
}
