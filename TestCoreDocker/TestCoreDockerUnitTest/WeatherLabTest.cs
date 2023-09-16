using System.ComponentModel;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Moq;
using TestCoreDockerService.Models.Options;
using TestCoreDockerService.Service;

namespace TestCoreDockerUnitTest;

[Trait("Category", "WeatherService")]
public sealed class WeatherLabTest
{
    private readonly Mock<IHttpClientFactory> _httpClientFactory;

    public WeatherLabTest()
    {
        _httpClientFactory = new Mock<IHttpClientFactory>();
    }
    [Fact]
    [Category()]
    public void WeatherShouldReturnASunnyWeatherValueTest()
    {

        var options = Options.Create(new WeatherOptions()
        {
            WeatherType = "Sunny",
            ForecastArea = "India"
        });
        var weatherLab = new WeatherLab(options,_httpClientFactory.Object);
        // note how we pass the 'Expected' value as the first argument,
        // then the actual value as the second argument, this will result
        // in any failures having a nicer error message
        Assert.Equal("Sunny weather", weatherLab.GetWeather().Summary);
    }
    
    
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