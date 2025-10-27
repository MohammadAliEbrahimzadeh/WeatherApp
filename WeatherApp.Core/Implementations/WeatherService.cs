using System.Net;
using WeatherApp.Core.Contracts;
using WeatherApp.Core.DTOs;
using WeatherApp.DTOs;
using WeatherApp.Core.Helpers;

namespace WeatherApp.Core.Implementations;

public class WeatherService : IWeatherService
{
    private readonly IWeatherDataProvider _weatherDataProvider;

    public WeatherService(IWeatherDataProvider weatherDataProvider)
    {
        _weatherDataProvider = weatherDataProvider;
    }

    public async Task<CustomResponse<WeatherSummaryDto>> GetCityWeatherAsync(CityQueryDto dto, CancellationToken cancellationToken)
    {
        var externalResult = await _weatherDataProvider.GetCurrentWeatherAsync(dto.CityName!, cancellationToken);

        if (externalResult is null)
            return CustomResponse<WeatherSummaryDto>.Failure("Unable to fetch weather data.", HttpStatusCode.BadGateway);

        if (externalResult.Current is null || externalResult.Location is null)
            return CustomResponse<WeatherSummaryDto>.Failure("Incomplete weather data received.", HttpStatusCode.InternalServerError);

        var air = externalResult.Current.AirQuality;

        var summary = new WeatherSummaryDto
        {
            City = externalResult.Location.Name ?? dto.CityName!,
            Latitude = externalResult.Location.Latitude ?? 0,
            Longitude = externalResult.Location.Longitude ?? 0,
            TemperatureCelsius = externalResult.Current.TempC ?? 0,
            Humidity = externalResult.Current.Humidity ?? 0,
            WindSpeedMps = externalResult.Current.WindKph.ConvertToMetersPerSecond(),
            AirQuality = new AirQualityDto
            {
                AirQualityIndex = air?.UsEpaIndex ?? 0,
                Description = air?.UsEpaIndex.GetAirQualityDescription(),
                PM2_5 = air?.Pm2_5,
                PM10 = air?.Pm10,
                CO = air?.Co,
                NO2 = air?.No2,
                SO2 = air?.So2,
                O3 = air?.O3
            }
        };

        return CustomResponse<WeatherSummaryDto>.Success(summary, HttpStatusCode.OK);
    }
}
