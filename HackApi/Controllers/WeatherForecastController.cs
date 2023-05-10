using Microsoft.AspNetCore.Mvc;

namespace HackApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ITimeService _timeService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration, ITimeService timeService)
    {
        _logger = logger;
        _configuration = configuration;
        _timeService = timeService;
    }

    [HttpGet()]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = _timeService.GetCurrentTime().AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
            .ToArray();
    }

    [HttpGet("One")]
    public WeatherForecast GetOne()
    {
        return new WeatherForecast
        {
            Date = _timeService.GetCurrentTime(),
            TemperatureC = 10,
            Summary = "Cool"
        };
    }

    [HttpGet("Two")]
    public WeatherForecast GetTwo()
    {
        return new WeatherForecast
        {
            Date = _timeService.GetCurrentTime(),
            TemperatureC = 10,
            Summary = _configuration.GetConnectionString("foodb")
        };
    }
}

public interface ITimeService
{
    DateTime GetCurrentTime();
}

public class TimeService : ITimeService
{
    public DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }
}