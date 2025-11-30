using APLTechnical.Infrastructure.Extensions;

namespace APLTechnical.Api.Extensions;

public static class ServiceCollectionExtensions
{
    private const string ConfigPrefix = "APLTechnical";

    public static IServiceCollection AddAplInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration.GetSection(ConfigPrefix));

        return services;
    }
}
