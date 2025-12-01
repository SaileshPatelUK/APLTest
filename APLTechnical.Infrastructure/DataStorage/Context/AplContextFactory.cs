using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace APLTechnical.Infrastructure.DataStorage.Context;

public class AplContextFactory : IDesignTimeDbContextFactory<AplContext>
{
    public AplContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "APLTechnical.Api");

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = config["APLTechnical:SqlConnectionString"]
            ?? throw new InvalidOperationException("Connection string not found.");

        var optionsBuilder = new DbContextOptionsBuilder<AplContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AplContext(optionsBuilder.Options);
    }
}
