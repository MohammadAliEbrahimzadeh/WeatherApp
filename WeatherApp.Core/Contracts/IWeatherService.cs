using WeatherApp.Core.DTOs;
using WeatherApp.DTOs;

namespace WeatherApp.Core.Contracts;

public interface IWeatherService
{
    Task<CustomResponse<WeatherSummaryDto>> GetCityWeatherAsync(CityQueryDto dto, CancellationToken cancellationToken);
}
