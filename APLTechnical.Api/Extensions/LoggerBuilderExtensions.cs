namespace APLTechnical.Api.Extensions;
public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder AddAplLogging(this ILoggingBuilder logging)
    {
        logging.AddApplicationInsights(
            configureTelemetryConfiguration: _ =>
            {
                // optional extra config
            },
            configureApplicationInsightsLoggerOptions: _ => { }
        );

        logging.AddFilter<
            Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider
        >(string.Empty, LogLevel.Information);

        return logging;
    }
}
