using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.DataAcessObject;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) 
        {
            optionsBuilder.UseInMemoryDatabase("MemoryDatabase");
        }
    }

    public DbSet<Fatura> Fatura { get; set; }
    public DbSet<FaturaItem> FaturaItem { get; set; }
}
