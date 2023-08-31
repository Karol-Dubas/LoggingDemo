using Microsoft.AspNetCore.Mvc;

namespace LoggingDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<DemoController> _logger; // type argument is logging category

    public DemoController(ILogger<DemoController> logger)
    {
        _logger = logger;
    }

    [HttpGet("forecast")]
    public IEnumerable<WeatherForecast> GetForecast()
    {
        _logger.LogInformation("Info");

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("logs")]
    public void Logs()
    {
        _logger.LogInformation(420, "Hello");
        // LoggingDemo.Controllers.WeatherForecastController[420]  <-- eventId


        _logger.LogTrace("Trace"); // least significant, very detailed, even might contain secrets
        _logger.LogDebug("Debug");
        _logger.LogInformation("Info"); // standard, how app is being used
        _logger.LogWarning("Warning"); // exceptions, expected error, shouldn't happen 
        _logger.LogError("Error"); // exceptions that might crash app, unexpected
        _logger.LogCritical("Critical"); // most significant, app is crashing


        _logger.LogInformation($"Current date {DateTimeOffset.Now}"); // it's converted to a string
        _logger.LogInformation("Current date {Date}", DateTimeOffset.Now); // to store log data as "object"


        try
        {
            throw new LackOfKnowledgeException("You can do better");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Caught exception:");
            _logger.LogError("Caught exception: {Exception}", e);
            _logger.LogError("Caught exception: {Message}", e.Message);
        }
    }

    class LackOfKnowledgeException : Exception
    {
        public LackOfKnowledgeException(string message) : base(message) { }
    }
}