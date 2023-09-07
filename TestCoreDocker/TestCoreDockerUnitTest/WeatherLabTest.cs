using TestCoreDockerService.Service;

namespace TestCoreDockerUnitTest
{
    public class WeatherLabTest
    {
        
        [Fact]
        public void NotNull() => Assert.NotNull(new WeatherLab("Sunny").GetWeather());
        [Fact]
        public void NotEmpty() => Assert.False(string.IsNullOrEmpty( new WeatherLab("Sunny").GetWeather().Summary));

        [Fact]
        public void NormalWeather() => Assert.Equal(new WeatherLab(null).GetWeather().Summary, "Normal weather");
        [Fact]
        public void SunnyWeather() => Assert.Equal(new WeatherLab("Sunny").GetWeather().Summary, "Sunny weather");
    }
}