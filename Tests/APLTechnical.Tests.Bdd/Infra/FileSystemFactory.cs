using APLTechnical.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace APLTechnical.Tests.Bdd.Infra;
public class FileSystemApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(
            [
                new KeyValuePair<string, string?>("ImageStorage:Provider", "FileSystem"),
            ]);
        });
    }
}
