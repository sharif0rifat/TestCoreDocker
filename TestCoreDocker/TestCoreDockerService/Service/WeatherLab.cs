﻿using TestCoreDockerService.Models.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using TestCoreDockerService.Helper;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;

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
            string weatherType = _options.ForecastType == "Forecast" ? "forecast.json" : "current.json";
            
            using (var client = _httpClientFactory.CreateClient())
            {
                string query = _options.ForecastType == "Forecast" ? $"?q={areaName}&days=1" : $"?q={areaName}";
                string callingUrl = $"{_options.ApiBaseUrl}/{weatherType}{query}&key={_options.ApiKey}";
                var response = await client.GetAsync(new Uri(callingUrl));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var resultStr = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(resultStr);
                    if (!IsNotNull(apiResponse))
                        //This will handled by the 'GlobalExceptionHandlingMiddleware'
                        throw new Exception("Some data mapping problem happened.");
                    return apiResponse;
                }
                else
                {
                    _logger.LogError("Erro occured while fetching weather api.", response);
                    throw new Exception("Erro occured while fetching weather api.");   //This will short-circuit the calling chanel and will be handled by the 'GlobalExceptionHandlingMiddleware'
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;   // This will handled by the 'GlobalExceptionHandlingMiddleware'
        }
    }
}