using Microsoft.EntityFrameworkCore;

namespace Stock.API.Models;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> context):base(context)
    {
    }

    public DbSet<Stock> Stocks { get; set; }
}