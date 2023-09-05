using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

Log.Information("Static logger");

// Write Serilog error config to console
Serilog.Debugging.SelfLog.Enable(Console.Error);

// Use Serilog as ILogger
builder.Host.UseSerilog((context, config) => config
    .ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("Injected logger, {Date:HH:mm}", DateTime.Now);
    Log.Information("Static logger in endpoint, {RandomNumber:P}", Random.Shared.NextDouble());
    
    logger.LogTrace("Trace");
    logger.LogDebug("Debug");
    logger.LogInformation("Info");
    logger.LogWarning("Warning"); 
    logger.LogError("Error");
    logger.LogCritical("Critical");
    
    return "Hello World!";
});

Task.Run(async () =>
{
    await Task.Delay(10);
    await new HttpClient().GetAsync("http://localhost:5035/");
});

app.Run();

