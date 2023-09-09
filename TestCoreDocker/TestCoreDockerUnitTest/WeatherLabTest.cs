using TestCoreDockerService.Service;

namespace TestCoreDockerUnitTest
{
    public class WeatherLabTest
    {
        
        [Fact]
        public void NotNull() => Assert.NotNull(new WeatherLab("Sunny","India").GetWeather());
        [Fact]
        public void NotEmpty() => Assert.False(string.IsNullOrEmpty( new WeatherLab("Sunny", "India").GetWeather().Summary));

        [Fact]
        public void NormalWeather() => Assert.Equal(new WeatherLab(null,null).GetWeather().Summary, "Normal weather");
        [Fact]
        public void SunnyWeather() => Assert.Equal(new WeatherLab("Sunny", "Bangladesh").GetWeather().Summary, "Sunny weather");
    }
}