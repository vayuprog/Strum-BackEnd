using Microsoft.AspNetCore.Mvc;
using Strum.Core.Entities;

namespace Strum_Back.Controllers;

[ApiController]
[Route("api")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]

    public IEnumerable<WeatherForecast> Get()
    {

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost(Name = "GetUser")]

    public User GetUser()
    {
        return new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = ""
        };
        // [HttpGet(Name = "test")]
        // public IEnumerable<String> Test()
        // {
        //     return Enumerable.Range(1, 5).Select(index => "test");
        // }
    }
}

