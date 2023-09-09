using System;

namespace TestCoreDockerService.Service
{
    public class WeatherLab: IWeatherLab
    {
        public string WeatherType { get; }
        public string ForcastArea { get; }
        public WeatherLab(string weatherType, string forcastArea)
        {
            WeatherType = weatherType;
            ForcastArea = forcastArea;
        }



        public WeatherForecast GetWeather()
        {
            if (WeatherType == "Sunny")
                return new WeatherForecast
                    (
                    DateTime.Now.AddDays(1),
                    Random.Shared.Next(-20, 55),
                    this.ForcastArea,
                    "Sunny weather"
                    );

            return new WeatherForecast
                (
                DateTime.Now.AddDays(1),                
                Random.Shared.Next(-20, 55),
                this.ForcastArea,
                "Normal weather"
                );
        }
    }
    public record WeatherForecast(DateTime Date, int TemperatureC,string Area, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}