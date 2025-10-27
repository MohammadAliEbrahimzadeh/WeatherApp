using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Text.Json;
using WeatherApp.DataModels;

public class WeatherDataProvider
{
    private readonly RestClient _client;
    private readonly string _apiKey;

    public WeatherDataProvider(IConfiguration configuration)
    {
        var baseUrl = configuration["WeatherApi:BaseUrl"]
            ?? throw new ArgumentNullException("WeatherApi:BaseUrl is missing from configuration");

        _apiKey = configuration["WeatherApi:ApiKey"]
            ?? throw new ArgumentNullException("WeatherApi:ApiKey is missing from configuration");

        _client = new RestClient(baseUrl);
    }

    public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city, CancellationToken cancellationToken)
    {
        var request = new RestRequest("current.json");
        request.AddQueryParameter("key", _apiKey);
        request.AddQueryParameter("q", city);
        request.AddQueryParameter("aqi", "yes");

        var response = await _client.ExecuteAsync(request, cancellationToken);

        if (!response.IsSuccessful || response.Content == null)
            return null;

        return JsonSerializer.Deserialize<WeatherResponse>(
            response.Content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
    }
}
