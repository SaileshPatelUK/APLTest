using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace APLTechnical.Infrastructure.DataStorage.Context;

public class AplContextFactory : IDesignTimeDbContextFactory<AplContext>
{
    public AplContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("APLTechnical:SqlConnectionString");

        var optionsBuilder = new DbContextOptionsBuilder<AplContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AplContext(optionsBuilder.Options);
    }
}
