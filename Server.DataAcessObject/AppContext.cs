using Microsoft.EntityFrameworkCore;
using Server.Entities;
namespace TesteDengine;

public class AppContext: DbContext
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {

    }

    public DbSet<Fatura> Fatura { get; set; }
    public DbSet<FaturaItem> FaturaItem{ get; set; }
        
}