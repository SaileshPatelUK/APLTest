using APLTechnical.Infrastructure.Extensions;
using APLTechnical.Services.Extensions;

namespace APLTechnical.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAplDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration);
        services.AddServiceLayerDependencies();
        return services;
    }
}
