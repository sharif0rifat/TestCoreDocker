using Microsoft.Extensions.DependencyInjection;
using TestCoreDockerService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string weatherType= Environment.GetEnvironmentVariable("WeatherType");
builder.Services.AddTransient<IWeatherLab>(x => new WeatherLab(weatherType));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (IWeatherLab lab) =>
{
    var forecast = lab.GetWeather();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();
