using System.ComponentModel.DataAnnotations;

namespace TestCoreDockerService.Models.Options;

public class WeatherOptions
{
    public const string OptionsKey = "WeatherOptions";
    
    [Required] [MinLength(1)] [MaxLength(260)]
    public string WeatherType { get; init; } = "";
    [Required] [MinLength(3)] [MaxLength(260)]
    public string ForecastArea { get; init; } = "";
}