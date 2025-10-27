using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core.Helpers;

public static class Converters
{
    public static double ConvertToMetersPerSecond(this double? windKph)
    {
        if (!windKph.HasValue) return 0;
        return Math.Round(windKph.Value / 3.6, 2);
    }

    public static string GetAirQualityDescription(this int? index)
    {
        return index switch
        {
            1 => "Good",
            2 => "Moderate",
            3 => "Unhealthy for sensitive groups",
            4 => "Unhealthy",
            5 => "Very Unhealthy",
            6 => "Hazardous",
            _ => "Unknown"
        };
    }

}
