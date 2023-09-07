using System;

namespace TestCoreDockerService.Service
{
    public class WeatherLab: IWeatherLab
    {
        public string WeatherType { get; }
        public WeatherLab(string weatherType)
        {
            WeatherType = weatherType;
        }



        public WeatherForecast GetWeather()
        {
            if (WeatherType == "Sunny")
                return new WeatherForecast
                    (
                    DateTime.Now.AddDays(1),
                    Random.Shared.Next(-20, 55),
                    "Sunny weather"
                    );

            return new WeatherForecast
                (
                DateTime.Now.AddDays(1),
                Random.Shared.Next(-20, 55),
                "Normal weather"
                );
        }
    }
    public record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}