using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

public class WeatherApiHealthCheck : IHealthCheck
{
    private readonly string _url;
    private readonly IConfiguration _configuration;

    public WeatherApiHealthCheck(IConfiguration configuration)
    {
        _configuration = configuration;
        var baseUrl = configuration["WeatherApi:BaseUrl"];
        var apiKey = configuration["WeatherApi:ApiKey"];
        var city = configuration["WeatherApi:City"];
        var aqi = configuration["WeatherApi:AQI"];

        _url = $"{baseUrl}?key={apiKey}&q={city}&aqi={aqi}";
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new RestClient(_url);
            var request = new RestRequest();
            var response = await client.ExecuteAsync(request, cancellationToken);

            if (!response.IsSuccessful)
            {
                return HealthCheckResult.Unhealthy($"HTTP Status: {response.StatusCode}");
            }

            return HealthCheckResult.Healthy("WeatherAPI is healthy");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"Exception: {ex.Message}");
        }
    }
}
