using Microsoft.AspNetCore.HttpLogging;
using TestCoreDockerService.Models.Options;
using TestCoreDockerService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add configuration to the container
// This will read from the included appsettings.json file in
// the first instance, however if there is an
// environment variable over-riding any of those values,
// then those Environment values will be used instead
// Example: export WeatherOptions__WeatherType="Sunny" will override the WeatherType value in appsettings.json
builder.Services.AddOptions<WeatherOptions>()
    .Bind(builder.Configuration.GetSection(WeatherOptions.OptionsKey))
    .ValidateDataAnnotations() // this will validate the WeatherOptions class using the DataAnnotations attributes
    .ValidateOnStart(); // this will ensure we validate when the app starts instead of waiting for the service to be called and then erroring. We do this as best to fail fast when inflating a production service instead of waiting for a customer to call the service and then erroring

// Add the weather service, its constructor will be passed the WeatherOptions we read from appsettings, and from the Environment
// using the IOptions pattern
builder.Services.AddTransient<IWeatherLab, WeatherLab>();
builder.Services.AddHttpLogging(log => log.LoggingFields = HttpLoggingFields.All); //This will log all the http calls
builder.Services.AddHttpClient();   //This is to use HTTPClient Factory  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()|| app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", (IWeatherLab lab, ILogger<Program> logger) =>
{
    logger.LogInformation("GetWeatherForecast called");
    return lab.GetWeather();
}).WithName("GetWeatherForecast");

app.Run();
