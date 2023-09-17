namespace TestCoreDockerService.Models.WeatherModels;

public record WeatherForecast(DateTime Date, int TemperatureC, string Area, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}