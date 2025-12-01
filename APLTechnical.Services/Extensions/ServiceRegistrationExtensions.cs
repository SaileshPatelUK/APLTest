using APLTechnical.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace APLTechnical.Services.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddServiceLayerDependencies(
                this IServiceCollection services)
    {
        // Service layer service registrations go here
        services.AddScoped<IImageService, ImageService>();
        return services;
    }
}
