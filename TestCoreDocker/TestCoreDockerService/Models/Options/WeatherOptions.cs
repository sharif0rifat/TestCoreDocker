using System.ComponentModel.DataAnnotations;

namespace TestCoreDockerService.Models.Options;

public class WeatherOptions
{
    public const string OptionsKey = "WeatherOptions";

    [Required] [MinLength(1)] [MaxLength(260)]
    public string WeatherType { get; init; } = "";
    [Required] [MinLength(3)] [MaxLength(260)]
    public string ForecastArea { get; init; } = "";
    /// <summary>
    /// This value is set to current by defalt from appsettings, hence get the current weather data
    /// If it is overriden by environment variable then get current weather data and 1 day ahead forecast 
    /// 1 day ahead forecast is hard-coded to keep it simple
    /// </summary>
    [Required] [MinLength(3)] [MaxLength(100)]
    public string ForecastType { get; set; } = "";  
    [Required] [MinLength(3)] [MaxLength(1000)]
    public string ApiKey { get; set; } = "";
    [Required] [MinLength(3)] [MaxLength(1000)]
    public string ApiBase { get; set; } = "";
}