using APLTechnical.Api.Extensions;
using APLTechnical.Infrastructure.Configuration;

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

            // Load "AplTechnical" section from appsettings.json
            var aplSection = builder.Configuration.GetSection("APLTechnical");

            builder.Services.Configure<AplTechnicalConfiguration>(aplSection);

            builder.Services.AddAplDependencies(builder.Configuration);

            // Logging
            builder.Logging.AddAplLogging();

            // API
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowVite",
                    policy => policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowVite");
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
