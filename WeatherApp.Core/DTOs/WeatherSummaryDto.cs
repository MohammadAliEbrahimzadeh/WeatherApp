namespace WeatherApp.DTOs;

/// <summary>
/// Represents a simplified view of the weather and air quality information
/// returned to the API consumer.
/// </summary>
public class WeatherSummaryDto
{
    /// <summary>
    /// Temperature in Celsius.
    /// </summary>
    public double? TemperatureCelsius { get; set; }

    /// <summary>
    /// Relative humidity percentage.
    /// </summary>
    public int? Humidity { get; set; }

    /// <summary>
    /// Wind speed in meters per second (m/s).
    /// </summary>
    public double? WindSpeedMps { get; set; }

    /// <summary>
    /// Major pollutants (μg/m³).
    /// </summary>
    public AirQualityDto? AirQuality { get; set; }

    /// <summary>
    /// Geographic coordinates of the city.
    /// </summary>
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    /// <summary>
    /// Optional: city name for clarity.
    /// </summary>
    public string? City { get; set; }
}

/// <summary>
/// Encapsulates all air quality–related measurements.
/// </summary>
public class AirQualityDto
{
    public int? AirQualityIndex { get; set; }
    public string? Description { get; set; }

    // Pollutants (μg/m³)
    public double? PM2_5 { get; set; }
    public double? PM10 { get; set; }
    public double? CO { get; set; }
    public double? NO2 { get; set; }
    public double? SO2 { get; set; }
    public double? O3 { get; set; }
}
