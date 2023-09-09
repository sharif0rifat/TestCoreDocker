using Microsoft.Extensions.DependencyInjection;
using TestCoreDockerService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Get the environment variable(That set in docker file) and inject to the service
string weatherType= Environment.GetEnvironmentVariable("WeatherType");
string forcastArea = builder.Configuration.GetValue<string>("ForecastArea");
builder.Services.AddTransient<IWeatherLab>(x => new WeatherLab(weatherType,forcastArea));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", (IWeatherLab lab) =>
{
    var forecast = lab.GetWeather();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();
