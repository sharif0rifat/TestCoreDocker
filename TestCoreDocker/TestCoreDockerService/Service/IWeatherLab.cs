using TestCoreDockerService.Models.Options;

namespace TestCoreDockerService.Service;

public interface IWeatherLab
{
    WeatherForecast GetWeather();
    Task<ApiResponse?> GetWeather(string areaName);
}