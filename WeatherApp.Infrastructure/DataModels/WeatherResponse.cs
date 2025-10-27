using System.Text.Json.Serialization;

namespace WeatherApp.DataModels;

/// <summary>
/// Represents the root object of the WeatherAPI.com /current.json response.
/// All properties are nullable to safely handle missing or null values during deserialization.
/// </summary>
public class WeatherResponse
{
    [JsonPropertyName("location")]
    public Location? Location { get; set; }

    [JsonPropertyName("current")]
    public Current? Current { get; set; }
}

/// <summary>
/// Contains geographical and time zone information.
/// </summary>
public class Location
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("lat")]
    public double? Latitude { get; set; }

    [JsonPropertyName("lon")]
    public double? Longitude { get; set; }

    [JsonPropertyName("tz_id")]
    public string? TimeZoneId { get; set; }

    [JsonPropertyName("localtime_epoch")]
    public long? LocalTimeEpoch { get; set; }

    [JsonPropertyName("localtime")]
    public string? LocalTime { get; set; }
}

/// <summary>
/// Contains the real-time weather and air quality data.
/// </summary>
public class Current
{
    [JsonPropertyName("last_updated_epoch")]
    public long? LastUpdatedEpoch { get; set; }

    [JsonPropertyName("last_updated")]
    public string? LastUpdated { get; set; }

    [JsonPropertyName("temp_c")]
    public double? TempC { get; set; }

    [JsonPropertyName("temp_f")]
    public double? TempF { get; set; }

    [JsonPropertyName("is_day")]
    public int? IsDay { get; set; }

    [JsonPropertyName("condition")]
    public Condition? Condition { get; set; }

    [JsonPropertyName("wind_mph")]
    public double? WindMph { get; set; }

    [JsonPropertyName("wind_kph")]
    public double? WindKph { get; set; }

    [JsonPropertyName("wind_degree")]
    public int? WindDegree { get; set; }

    [JsonPropertyName("wind_dir")]
    public string? WindDir { get; set; }

    [JsonPropertyName("pressure_mb")]
    public double? PressureMb { get; set; }

    [JsonPropertyName("pressure_in")]
    public double? PressureIn { get; set; }

    [JsonPropertyName("precip_mm")]
    public double? PrecipMm { get; set; }

    [JsonPropertyName("precip_in")]
    public double? PrecipIn { get; set; }

    [JsonPropertyName("humidity")]
    public int? Humidity { get; set; }

    [JsonPropertyName("cloud")]
    public int? Cloud { get; set; }

    [JsonPropertyName("feelslike_c")]
    public double? FeelslikeC { get; set; }

    [JsonPropertyName("feelslike_f")]
    public double? FeelslikeF { get; set; }

    [JsonPropertyName("windchill_c")]
    public double? WindchillC { get; set; }

    [JsonPropertyName("windchill_f")]
    public double? WindchillF { get; set; }

    [JsonPropertyName("heatindex_c")]
    public double? HeatindexC { get; set; }

    [JsonPropertyName("heatindex_f")]
    public double? HeatindexF { get; set; }

    [JsonPropertyName("dewpoint_c")]
    public double? DewpointC { get; set; }

    [JsonPropertyName("dewpoint_f")]
    public double? DewpointF { get; set; }

    [JsonPropertyName("vis_km")]
    public double? VisKm { get; set; }

    [JsonPropertyName("vis_miles")]
    public double? VisMiles { get; set; }

    [JsonPropertyName("uv")]
    public double? Uv { get; set; }

    [JsonPropertyName("gust_mph")]
    public double? GustMph { get; set; }

    [JsonPropertyName("gust_kph")]
    public double? GustKph { get; set; }

    [JsonPropertyName("air_quality")]
    public AirQuality? AirQuality { get; set; }

    [JsonPropertyName("short_rad")]
    public double? ShortRad { get; set; }

    [JsonPropertyName("diff_rad")]
    public double? DiffRad { get; set; }

    [JsonPropertyName("dni")]
    public double? Dni { get; set; }

    [JsonPropertyName("gti")]
    public double? Gti { get; set; }
}

/// <summary>
/// Details about the current weather condition (text, icon, code).
/// </summary>
public class Condition
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("code")]
    public int? Code { get; set; }
}

/// <summary>
/// Details about current air quality and pollutant concentrations.
/// Note the use of [JsonPropertyName] for fields containing hyphens.
/// </summary>
public class AirQuality
{
    [JsonPropertyName("co")]
    public double? Co { get; set; }

    [JsonPropertyName("no2")]
    public double? No2 { get; set; }

    [JsonPropertyName("o3")]
    public double? O3 { get; set; }

    [JsonPropertyName("so2")]
    public double? So2 { get; set; }

    [JsonPropertyName("pm2_5")]
    public double? Pm2_5 { get; set; }

    [JsonPropertyName("pm10")]
    public double? Pm10 { get; set; }

    [JsonPropertyName("us-epa-index")]
    public int? UsEpaIndex { get; set; }

    [JsonPropertyName("gb-defra-index")]
    public int? GbDefraIndex { get; set; }
}
