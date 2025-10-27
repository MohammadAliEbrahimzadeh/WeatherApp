using WeatherApp.DataModels;

public interface IWeatherDataProvider
{
    Task<WeatherResponse?> GetCurrentWeatherAsync(string city, CancellationToken cancellationToken);
}
