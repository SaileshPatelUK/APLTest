using APLTechnical.Infrastructure.DataStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace APLTechnical.Infrastructure.DataStorage.Context;
public class AplContext : DbContext
{
    public DbSet<ImageEntity> Images { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;");
}
