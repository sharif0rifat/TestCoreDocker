using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TestCoreDockerService.Models.Options;
using TestCoreDockerService.Service;

namespace TestCoreDockerUnitTest;

[Trait("Category", "WeatherService")]
public sealed class WeatherLabTest
{
    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private readonly WeatherLab _weatherLab;

    public WeatherLabTest()
    {
        _httpClientFactory = new Mock<IHttpClientFactory>();
        var logger = new Mock<ILogger<WeatherLab>>();
        var options = Options.Create(new WeatherOptions()
        {
            WeatherType = "Sunny",
            ForecastArea = "India"
        });
        _weatherLab = new WeatherLab(options, (ILogger<WeatherLab>)logger, _httpClientFactory.Object);
    }
    [Fact]
    [Category()]
    // note how we pass the 'Expected' value as the first argument,
    // then the actual value as the second argument, this will result
    // in any failures having a nicer error message
    public void WeatherShouldReturnASunnyWeatherValueTest()=> Assert.Equal("Sunny weather", _weatherLab.GetWeather().Summary);
    
    
    
    // [Fact]
    // public void NotNull() => Assert.NotNull(new WeatherLab("Sunny","India").GetWeather());
    // [Fact]
    // public void NotEmpty() => Assert.False(string.IsNullOrEmpty( new WeatherLab("Sunny", "India").GetWeather().Summary));
    //
    // [Fact]
    // public void NormalWeather() => Assert.Equal(new WeatherLab(null,null).GetWeather().Summary, "Normal weather");
    // [Fact]
    // public void SunnyWeather() => Assert.Equal(new WeatherLab("Sunny", "Bangladesh").GetWeather().Summary, "Sunny weather");
}