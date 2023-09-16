using TestCoreDockerService.Models.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace TestCoreDockerService.Service;

public class WeatherLab: IWeatherLab
{
    private readonly WeatherOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;

    public WeatherLab(IOptions <WeatherOptions> options, IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(options);
        _options = options.Value;
        _httpClientFactory= httpClientFactory;
    }

    public WeatherForecast GetWeather()
    {
        if (_options.WeatherType == "Sunny")
            return new WeatherForecast
            (
                DateTime.Now.AddDays(1),
                Random.Shared.Next(-20, 55),
                _options.ForecastArea,
                "Sunny weather"
            );

        return new WeatherForecast
        (
            DateTime.Now.AddDays(1),
            Random.Shared.Next(-20, 55),
            _options.ForecastArea,
            "Normal weather"
        );
    }

    public async Task<ApiResponse?> GetWeather(string areaName)
    {
        try
        {
            //Forecast//https://api.weatherapi.com/v1/forecast.json?q=Sydney&days=1&key=5b10539139e74191a0c204447231309
            //Current//
            string weatherType = "";
            weatherType = _options.ForecastType == "Forecast" ? "forecast.json" : "current.json";
            string callingUrl = //"https://api.weatherapi.com/v1/current.json?q=Sydney&lang=en&key=5b10539139e74191a0c204447231309";
                _options.ApiBaseUrl + "/" + weatherType + "?q=" + areaName + "&key=" + _options.ApiKey;
            using (var client = _httpClientFactory.CreateClient())
            {
                Uri callingUri = new Uri(callingUrl);
                var result = await client.GetAsync(callingUri);
                var resultStr = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(resultStr);
                return apiResponse;
            }
            //var client = _httpClientFactory.CreateClient();
            //var result = await client.GetAsync(callingUrl);
            //var resultStr = await result.Content.ReadAsStringAsync();
            //var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(resultStr);
            //return apiResponse;
        }
        catch (Exception)
        {
            return null;
        }

    }
}