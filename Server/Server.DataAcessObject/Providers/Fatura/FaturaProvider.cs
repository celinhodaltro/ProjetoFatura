using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.DataAcessObject.Providers;

public class FaturaProvider : BaseProvider<Fatura>
{
    public FaturaProvider(AppDbContext context) : base(context) { }

    public async Task<Fatura> GetFaturaWithItens(int faturaId)
    {
        var result = await _dbSet
            .Include(f => f.FaturaItem)
            .FirstOrDefaultAsync(f => f.Id == faturaId);

        if (result is null)
            return new Fatura();

        return result;
    }

    public async Task<List<Fatura>> BuscarFaturasComFiltros(FaturaFilter Filter)
    {
        IQueryable<Fatura> query = _dbSet;

        if (!string.IsNullOrEmpty(Filter.Cliente))
            query = query.Where(f => f.Cliente.Contains(Filter.Cliente));


        if (Filter.DateInitial.HasValue)
            query = query.Where(f => f.Data >= Filter.DateInitial.Value);

        
        if (Filter.DateFinish.HasValue)
            query = query.Where(f => f.Data <= Filter.DateFinish.Value);
        
        if (Filter is { PageSize:>0})
            query = query.Skip((Filter.Page - 1) * Filter.PageSize).Take(Filter.PageSize);
        

        return await query.ToListAsync();
    }

    public async Task<int> CountFaturasComFiltros(FaturaFilter filter)
    {
        var query = _context.Fatura.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Cliente))
        {
            query = query.Where(f => f.Cliente.Contains(filter.Cliente));
        }

        if (filter.DateInitial.HasValue)
        {
            query = query.Where(f => f.Data >= filter.DateInitial.Value);
        }

        if (filter.DateFinish.HasValue)
        {
            query = query.Where(f => f.Data <= filter.DateFinish.Value);
        }

        return await query.CountAsync();
    }

}
