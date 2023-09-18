using System.ComponentModel.DataAnnotations;

namespace TestCoreDockerService.Models.Options;

public class WeatherOptions
{
    public const string OptionsKey = "WeatherOptions";

    [Required] [MinLength(1)] [MaxLength(260)]
    public string WeatherType { get; init; } = "";
    [Required] [MinLength(3)] [MaxLength(260)]
    public string ForecastArea { get; init; } = "";
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string ForecastType { get; set; } = "";
    [Required]
    [MinLength(3)]
    [MaxLength(1000)]
    public string ApiKey { get; set; } = "";
    [Required]
    [MinLength(3)]
    [MaxLength(1000)]
    public string ApiBase { get; set; } = "";
}