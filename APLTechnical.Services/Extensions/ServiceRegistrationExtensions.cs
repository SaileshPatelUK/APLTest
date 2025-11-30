using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace APLTechnical.Services.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddServiceLayerRegistrations(
                this IServiceCollection services)
    {
        //var aplSection = configuration.GetSection("APLTechnical");

        // Service layer service registrations go here
        services.AddScoped<ImageService>();
        return services;
    }
}
