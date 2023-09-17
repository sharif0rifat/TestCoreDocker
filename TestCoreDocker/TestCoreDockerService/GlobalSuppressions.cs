// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "<Pending>", Scope = "member", Target = "~P:TestCoreDockerService.Models.Options.WeatherOptions.ApiBaseUrl")]
[assembly: SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>")]
[assembly: SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>", Scope = "member", Target = "~M:TestCoreDockerService.Service.WeatherLab.GetWeather(System.String)~System.Threading.Tasks.Task{TestCoreDockerService.Models.WeatherModels.ApiResponse}")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>", Scope = "member", Target = "~P:TestCoreDockerService.Models.WeatherModels.Forecast.forecastday")]
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "<Pending>", Scope = "member", Target = "~P:TestCoreDockerService.Models.WeatherModels.Forecast.forecastday")]
