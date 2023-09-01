var builder = WebApplication.CreateBuilder(args);

// Logging runs synchronously, because these are small operations.
// It shouldn't slow application, but can.

// Override default settings
builder.Logging.ClearProviders() // clear default logging configuration
    .AddConfiguration(builder.Configuration.GetSection("Logging")) // add appsettings.json
    .AddDebug() // debug window
    .AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Task.Run(async () =>
{
    await Task.Delay(10);
    await new HttpClient().GetAsync("http://localhost:5046/demo/logs");
});

app.Run();