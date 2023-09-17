using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCoreDockerService.Models.WeatherModels
{
    public class ApiResponse
    {
        public Location? location { get; set; }
        public Current? current { get; set; }
        public Forecast? forecast { get; set; }

    }

    public class Forecast
    {
#pragma warning disable CA1002 // Do not expose generic lists
#pragma warning disable CA2227 // Collection properties should be read only
        public List<Forecastday>? forecastday { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore CA1002 // Do not expose generic lists
    }

    public class Forecastday
    {
        public DateTime date { get; set; }
        public Day? day { get; set; }
    }

    public class Day
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores
        public float maxtemp_c { get; set; }

        public float maxtemp_f { get; set; }
        public float mintemp_c { get; set; }
        public float mintemp_f { get; set; }
        public float avgtemp_c { get; set; }
        public float avgtemp_f { get; set; }
        public float avghumidity { get; set; }
        public bool daily_will_it_rain { get; set; }
        public bool daily_will_it_snow { get; set; }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }

    public class Current
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores
        public float temp_c { get; set; }

        public float temp_f { get; set; }
        public float wind_mph { get; set; }
        public float wind_kph { get; set; }
        public float humidity { get; set; }
#pragma warning restore CA1707 // Identifiers should not contain underscores
        public Condition? condition { get; set; }

    }

    public class Condition
    {
        public string text { get; set; } = "";
        public string icon { get; set; } = "";
    }

    public class Location
    {
        public string Name { get; set; } = "";
        public string Region { get; set; } = "";
        public string country { get; set; } = "";
    }
}
