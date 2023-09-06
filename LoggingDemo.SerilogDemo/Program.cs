using LoggingDemo.SerilogDemo;
using Serilog;
using Serilog.Debugging;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

Log.Information("Static logger, {RandomNumber:P}", Random.Shared.NextDouble());

// Write Serilog error config to console
SelfLog.Enable(Console.Error);

// Use Serilog as ILogger
builder.Host.UseSerilog((context, config) => config
    .ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("Injected logger, {Date:HH:mm}", DateTime.Now);

    logger.LogTrace("Trace");
    logger.LogDebug("Debug");
    logger.LogInformation("Info");
    logger.LogWarning("Warning");
    logger.LogError("Error");
    logger.LogCritical("Critical");

    logger.LogInformation(new Exception("exception message"), "Log exception");

    logger.LogInformation("Person: {@Person}", new Person("Karol", 23)); // deconstruct

    //throw new Exception("hehe");

    return "Hello World!";
});

Task.Run(async () =>
{
    await Task.Delay(10);
    await new HttpClient().GetAsync("http://localhost:5035/");
});

app.Run();


// App can be wrapped with code like below:
try { }
catch (Exception ex)
{
    Log.Fatal(ex, "App terminated unexpectedly");
}
finally
{
    Console.WriteLine("App shutting down...");
    Log.CloseAndFlush();
}