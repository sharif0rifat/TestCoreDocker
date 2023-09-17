using TestCoreDockerService.Models.Weathers;

namespace TestCoreDockerService.Service;

public interface IWeatherLab
{
    WeatherForecast GetWeather();
    Task<ApiResponse?> GetWeather(string areaName);
}