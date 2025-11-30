using APLTechnical.Api.Extensions;

namespace APLTechnical.Api;

public class Program
{
    private static void Main(string[] args)
    {
        const string DEFAULT_CONFIG_PREFIX = "APLTechnical";

        using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
        var bootstrapLogger = loggerFactory.CreateLogger<Program>();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Infrastructure / DI
            builder.Services.AddAplInfrastructure(builder.Configuration);

            // 2. Logging
            builder.Logging.AddAplLogging();

            // 3. API
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            bootstrapLogger.LogError(
                ex,
                "{ServiceName} service unable to start. hostBuilder.Build() failed: {ExceptionMessage}",
                DEFAULT_CONFIG_PREFIX,
                ex.Message
            );
        }
    }
}
