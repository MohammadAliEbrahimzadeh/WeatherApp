using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using WeatherApp.Core.Contracts;
using WeatherApp.Core.DTOs;
using WeatherApp.Core.Implementations;
using WeatherApp.DataModels;
using WeatherApp.DTOs;
using Xunit;

namespace WeatherApp.Tests
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task GetCityWeatherAsync_ReturnsSuccessResponse_WhenDataIsValid()
        {
            // Arrange
            var fakeWeatherResponse = new WeatherResponse
            {
                Location = new Location
                {
                    Name = "Tehran",
                    Latitude = 35.6892,
                    Longitude = 51.3890
                },
                Current = new Current
                {
                    TempC = 28,
                    Humidity = 50,
                    WindKph = 10,
                    AirQuality = new AirQuality
                    {
                        UsEpaIndex = 2,
                        Pm2_5 = 12.5,
                        Pm10 = 20.0,
                        Co = 0.5,
                        No2 = 15,
                        So2 = 4,
                        O3 = 30
                    }
                }
            };

            var mockProvider = new Mock<IWeatherDataProvider>();
            mockProvider
                .Setup(p => p.GetCurrentWeatherAsync("Tehran", It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeWeatherResponse);

            // **Use the concrete implementation, not the interface**
            var service = new WeatherService(mockProvider.Object);

            var cityQuery = new CityQueryDto { CityName = "Tehran" };

            // Act
            var result = await service.GetCityWeatherAsync(cityQuery, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("Tehran", result.Data!.City);
            Assert.Equal(28, result.Data.TemperatureCelsius);
            Assert.Equal(50, result.Data.Humidity);

            var expectedWind = Math.Round(10 / 3.6, 2);
            Assert.Equal(expectedWind, result.Data.WindSpeedMps);


            Assert.NotNull(result.Data.AirQuality);
            Assert.Equal(2, result.Data.AirQuality!.AirQualityIndex);
            Assert.Equal(12.5, result.Data.AirQuality.PM2_5);
            Assert.Equal(20.0, result.Data.AirQuality.PM10);
        }


        [Fact]
        public async Task GetCityWeatherAsync_ReturnsFailureResponse_WhenProviderReturnsNull()
        {
            var mockProvider = new Mock<IWeatherDataProvider>();
            mockProvider
                .Setup(p => p.GetCurrentWeatherAsync("Tehran", It.IsAny<CancellationToken>()))
                .ReturnsAsync((WeatherResponse?)null);

            var service = new WeatherService(mockProvider.Object);
            var cityQuery = new CityQueryDto { CityName = "UnknownCity" };

            // Act
            var result = await service.GetCityWeatherAsync(cityQuery, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.BadGateway, result.StatusCode);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetCityWeatherAsync_ReturnsFailureResponse_WhenDataIsIncomplete()
        {
            // Arrange
            var mockProvider = new Mock<IWeatherDataProvider>();

            var incompleteResponse = new WeatherResponse
            {
                Location = null,
                Current = null
            };

            mockProvider
                .Setup(p => p.GetCurrentWeatherAsync("Tehran", It.IsAny<CancellationToken>()))
                .ReturnsAsync(incompleteResponse);

            var service = new WeatherService(mockProvider.Object);
            var cityQuery = new CityQueryDto { CityName = "Tehran" };

            // Act
            var result = await service.GetCityWeatherAsync(cityQuery, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Null(result.Data);
        }
    }
}
