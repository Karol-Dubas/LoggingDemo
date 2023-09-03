namespace LoggingDemo.SourceGenerator;

public static partial class LoggerExtensions
{
    [LoggerMessage(
        0,
        LogLevel.Information, 
        "Writing hello world response to {Person}")]
    public static partial void LogHelloWorld(this ILogger logger, Person person);
}