using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TestCoreDockerService.Helper;
using TestCoreDockerService.Models.Options;
using TestCoreDockerService.Service;

namespace TestCoreDockerUnitTest;


public sealed class WeatherLabTest : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private readonly IOptions<WeatherOptions> _options;
    private readonly WeatherLab _weatherLab;
    private readonly Mock<ILogger<WeatherLab>> _logger;

    public WeatherLabTest()
    {
        _httpClient = new HttpClient();
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(_httpClient);
        var logger = new Mock<ILogger<WeatherLab>>();
        _logger = logger;

        //Set default option 
        var options = Options.Create(new WeatherOptions()
        {
            WeatherType = "Sunny",
            ForecastArea = "India",
            ForecastType= "Current",
            ApiKey = "5b10539139e74191a0c204447231309",
            ApiBase= "https://api.weatherapi.com/v1"
        });
        _options = options;
        _weatherLab = new WeatherLab(options, logger.Object, _httpClientFactory.Object);
    }
    [Fact]
    [Trait("Category", "WeatherService")]
    // note how we pass the 'Expected' value as the first argument,
    // then the actual value as the second argument, this will result
    // in any failures having a nicer error message
    public void WeatherShouldReturnASunnyWeatherValueTest()=> Assert.Equal("Sunny weather", _weatherLab.GetWeather().Summary);

    [Fact]
    [Trait("Category", "WeatherAPI")]
    public void WeatherShouldReturnNonEmptyApiRespone() => Assert.True(_weatherLab.GetWeather("Sydney").IsNotNull());


    [Fact]
    [Trait("Category", "WeatherAPI")]
    public async void WeatherShouldReturnCountryNameAsAustralia()
    {
        var apiResponse = await _weatherLab.GetWeather("Sydney").ConfigureAwait(true);
        Assert.NotNull(apiResponse);
        if (!apiResponse.IsNotNull())
            Assert.Fail("Api Response is null");
        Assert.Equal("Australia", apiResponse.location?.country);
    }

    [Fact]
    [Trait("Category", "WeatherAPI")]
    public async void WeatherShouldReturnNonEmptyForecast()
    {
        //Override the 'ForecastType' to see forecast data
        _options.Value.ForecastType = "Forecast";
        var weatherLab = new WeatherLab(_options, _logger.Object, _httpClientFactory.Object);

        var apiResponse = await weatherLab.GetWeather("Sydney").ConfigureAwait(true);
        Assert.NotNull(apiResponse);
        if (!apiResponse.IsNotNull())
            Assert.Fail("Api Response is null");
        Assert.NotNull(apiResponse.forecast);
    }

    public  void Dispose()
    {
        _httpClient.Dispose();
    }
}