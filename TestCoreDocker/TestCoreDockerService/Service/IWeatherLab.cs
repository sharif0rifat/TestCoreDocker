using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCoreDockerService.Service
{
    public interface IWeatherLab
    {
        WeatherForecast GetWeather();
    }
}
