using LoggingDemo.SourceGenerator;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", ([FromServices]ILogger<Program> logger) =>
{
    // Source generated, structured log with great performance
    logger.LogHelloWorld(new Person("Karol", 23)); 
    
    return "Hello World!";
});

Task.Run(async () =>
{
    await Task.Delay(10);
    await new HttpClient().GetAsync("http://localhost:5104/");
});

app.Run();
