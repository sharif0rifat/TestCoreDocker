using System.ComponentModel;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TestCoreDockerService.Helper;
using TestCoreDockerService.Models.Options;
using TestCoreDockerService.Service;

namespace TestCoreDockerUnitTest;

[Trait("Category", "WeatherService")]
public sealed class WeatherLabTest
{
    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private readonly IOptions<WeatherOptions> _options;
    private readonly WeatherLab _weatherLab;
    private readonly Mock<ILogger<WeatherLab>> _logger;

    public WeatherLabTest()
    {
        _httpClientFactory = new Mock<IHttpClientFactory>();
        var logger = new Mock<ILogger<WeatherLab>>();
        _logger = logger;
        var options = Options.Create(new WeatherOptions()
        {
            WeatherType = "Sunny",
            ForecastArea = "India",
            ForecastType= "Current",
            ApiKey = "5b10539139e74191a0c204447231309",
            ApiBaseUrl= "https://api.weatherapi.com/v1"
        });
        _options = options;
        _weatherLab = new WeatherLab(options, logger.Object, _httpClientFactory.Object);
    }
    [Fact]
    [Category()]
    // note how we pass the 'Expected' value as the first argument,
    // then the actual value as the second argument, this will result
    // in any failures having a nicer error message
    public void WeatherShouldReturnASunnyWeatherValueTest()=> Assert.Equal("Sunny weather", _weatherLab.GetWeather().Summary);

    //"country": "Australia"
    [Fact]
    [Category()]
    public void WeatherShouldReturnNonEmptyApiRespone() => Assert.True(_weatherLab.GetWeather("Sydney").IsNotNull());


    [Fact]
    [Category()]
    public async void WeatherShouldReturnCountryNameAsAustralia()
    {
        var apiResponse= await _weatherLab.GetWeather("Sydney");
#pragma warning disable CS8604 // Possible null reference argument.
        if (!apiResponse.IsNotNull())
            Assert.Fail("Api Response is null");
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.Equal("Australia", apiResponse.location?.country);
    }

    [Fact]
    [Category()]
    public async void WeatherShouldReturnNonEmptyForecast()
    {
        _options.Value.ForecastType = "Forecast";
        var weatherLab = new WeatherLab(_options, _logger.Object, _httpClientFactory.Object);

        var apiResponse = await weatherLab.GetWeather("Sydney");
#pragma warning disable CS8604 // Possible null reference argument.
        if (!apiResponse.IsNotNull())
            Assert.Fail("Api Response is null");
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.NotNull(apiResponse.forecast);
    }
}