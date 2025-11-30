using APLTechnical.Core.Enums;
using APLTechnical.Infrastructure.DataStorage.Context;
using APLTechnical.Infrastructure.ImageStorage;
using APLTechnical.Infrastructure.ImageStorage.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APLTechnical.Infrastructure.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var aplSection = configuration.GetSection("APLTechnical");

        // Other infra registration (DbContext, BlobServiceClient, etc.)
        services.RegisterDataStorageConnection(aplSection);
        services.RegisterBlobStorageConnection(aplSection);

        services.AddTransient<BlobImageStorage>();
        services.AddTransient<FileSystemImageStorage>();

        // Single IImageStorage that chooses based on config
        services.AddTransient<IImageStorage>(sp =>
        {
            // Try both common keys: "APLTechnical:ImageStorage:Provider" and "APLTechnical:ImageStorageProvider"
            var providerString = aplSection.GetSection("ImageStorage")["Provider"]
                             ?? aplSection["ImageStorageProvider"];

            if (string.IsNullOrWhiteSpace(providerString))
            {
                throw new InvalidOperationException(
                    "Configuration value 'APLTechnical:ImageStorage:Provider' or 'APLTechnical:ImageStorageProvider' is not configured.");
            }

            if (!Enum.TryParse<ImageStorageProvider>(providerString, ignoreCase: true, out var provider))
            {
                throw new InvalidOperationException($"Unsupported ImageStorageProvider: {providerString}");
            }

            return provider switch
            {
                ImageStorageProvider.Blob =>
                    sp.GetRequiredService<BlobImageStorage>(),

                ImageStorageProvider.FileSystem =>
                    sp.GetRequiredService<FileSystemImageStorage>(),

                _ => throw new InvalidOperationException(
                    $"Unsupported ImageStorageProvider: {provider}")
            };
        });

        return services;
    }

    private static IServiceCollection RegisterDataStorageConnection(
        this IServiceCollection services,
        IConfiguration aplSection)
    {
        var connectionString = aplSection["SqlConnectionString"];

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Configuration value 'APLTechnical:SqlConnectionString' is not configured.");
        }

        services.AddDbContext<AplContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
                sqlOptions.MigrationsAssembly(typeof(AplContext).Assembly.FullName)));

        return services;
    }

    private static IServiceCollection RegisterBlobStorageConnection(
        this IServiceCollection services,
        IConfiguration aplSection)
    {
        var blobConnectionString = aplSection.GetSection("BlobStorage")["ConnectionString"];

        if (string.IsNullOrWhiteSpace(blobConnectionString))
        {
            throw new InvalidOperationException(
                "Configuration value 'APLTechnical:BlobStorage:ConnectionString' is not configured.");
        }

        // Register BlobServiceClient
        services.AddSingleton(new BlobServiceClient(blobConnectionString));

        // Optional: register your own wrapper service if you want abstraction
        // services.AddScoped<IBlobStorageService, BlobStorageService>();

        return services;
    }
}
