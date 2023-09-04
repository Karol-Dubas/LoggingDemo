using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

Log.Information("Static logger");

// Use Serilog as ILogger
 builder.Host.UseSerilog((context, config) =>
 {
     config.ReadFrom.Configuration(context.Configuration);
 });

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("Injected logger, {Date:HH:mm}", DateTime.Now);
    Log.Information("Static logger in endpoint, {RandomNumber:P}", Random.Shared.NextDouble());
    
    return "Hello World!";
});

app.Run();

