using APLTechnical.Api;
using APLTechnical.Infrastructure.DataStorage.Context;
using APLTechnical.Infrastructure.ImageStorage.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Inmemorydb for testing
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AplContext>));

            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.AddDbContext<AplContext>(options =>
            {
                options.UseInMemoryDatabase("AplTestDatabase");
            });

            services.AddSingleton<IImageStorage, FakeImageStorage>();

        });
    }
}
