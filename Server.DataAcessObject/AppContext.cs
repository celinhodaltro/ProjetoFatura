using Microsoft.EntityFrameworkCore;
namespace TesteDengine;

public class AppContext: DbContext
{
    public AppContext(DbContextOptions<DbExercicio4> options)
        : base(options)
    {

    }

    public DbSet<Fatura> Fatura { get; set; }
    public DbSet<FaturaItem> FaturaItem{ get; set; }
        
}