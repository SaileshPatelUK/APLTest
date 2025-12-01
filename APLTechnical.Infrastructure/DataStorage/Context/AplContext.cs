using APLTechnical.Infrastructure.DataStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace APLTechnical.Infrastructure.DataStorage.Context;

public class AplContext(DbContextOptions<AplContext> options) : DbContext(options)
{
    public DbSet<ImageEntity> Images { get; set; }
}
