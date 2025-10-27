using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherApp.Core.Contracts;
using WeatherApp.Core.DTOs;
using WeatherApp.DTOs;

namespace WeatherApp.Api.Controllers;

/// <summary>
/// Provides endpoints for retrieving weather and air quality data for a specific city.
/// </summary>
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastController"/>.
    /// </summary>
    /// <param name="weatherService">The service used to fetch weather information.</param>
    public WeatherForecastController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    /// <summary>
    /// Retrieves the current weather and air quality data for a given city.
    /// </summary>
    /// <remarks>
    /// **Usage Example:**  
    /// `GET /WeatherForecast/getWeather?cityName=Tehran`  
    ///  
    /// The response includes temperature, humidity, wind speed, air quality index (AQI),
    /// major pollutants, and geographic coordinates.
    /// </remarks>
    /// <param name="dto">The query containing the city name (e.g., Tehran).</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// Returns a <see cref="CustomResponse{T}"/> containing <see cref="WeatherSummaryDto"/> 
    /// with weather and air quality information.
    /// </returns>
    /// <response code="200">Weather and air quality data retrieved successfully.</response>
    /// <response code="400">Bad request (e.g., invalid city name).</response>
    /// <response code="502">External weather API unavailable or failed to respond.</response>
    /// <response code="500">An unexpected error occurred while fetching or processing data.</response>
    [HttpGet("getWeather")]
    [ProducesResponseType(typeof(CustomResponse<WeatherSummaryDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CustomResponse<WeatherSummaryDto>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(CustomResponse<WeatherSummaryDto>), (int)HttpStatusCode.BadGateway)]
    [ProducesResponseType(typeof(CustomResponse<WeatherSummaryDto>), (int)HttpStatusCode.InternalServerError)]
    [Produces("application/json")]
    public async Task<CustomResponse<WeatherSummaryDto>> GetWeatherAsync(
        [FromQuery] CityQueryDto dto,
        CancellationToken cancellationToken)
    {
        return await _weatherService.GetCityWeatherAsync(dto, cancellationToken);
    }
}
