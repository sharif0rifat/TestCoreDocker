using TestCoreDockerService.Models.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using TestCoreDockerService.Models.WeatherModels;

namespace TestCoreDockerService.Service;

public class WeatherLab: IWeatherLab
{
    private readonly WeatherOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<WeatherLab> _logger;

    private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;
    public WeatherLab(IOptions <WeatherOptions> options,ILogger<WeatherLab> logger, IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(options);
        _options = options.Value;
        _httpClientFactory= httpClientFactory;
        _logger = logger;
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
            //By default forcast type is current,
            //if it is overriden by environment variable then get 1 day ahead forecast data
            string forecastType = _options.ForecastType == "Forecast" ? "forecast.json" : "current.json";
            
            using (var client = _httpClientFactory.CreateClient())
            {
                string query = _options.ForecastType == "Forecast" ? $"?q={areaName}&days=1" : $"?q={areaName}";
                string callingUrl = $"{_options.ApiBase}/{forecastType}{query}&key={_options.ApiKey}";
                var response = await client.GetAsync(new Uri(callingUrl)).ConfigureAwait(true);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var resultStr = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(resultStr);
                    if (!IsNotNull(apiResponse))
                        throw new InvalidOperationException("Some data mapping problem happened."); //This will handled by the 'GlobalExceptionHandlingMiddleware'
                    return apiResponse;
                }
                else
                {
                    _logger.LogError("Erro occured while fetching weather api.", response);
                    throw new HttpRequestException("Erro occured while fetching weather api.");   //Throwing Exception intentionally to be handled by the 'GlobalExceptionHandlingMiddleware'
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Some error happened while Getting weather data");
            throw new InvalidOperationException("Some error happened while Getting weather data");   // This will handled by the 'GlobalExceptionHandlingMiddleware'
        }
    }}