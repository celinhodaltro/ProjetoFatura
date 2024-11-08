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

    public async Task<IEnumerable<RelatorioCliente>> GerarRelatorioPorCliente(string cliente)
    {
        var query = _context.Fatura.AsQueryable();

        if (!string.IsNullOrEmpty(cliente))
        {
            query = query.Where(f => f.Cliente.Contains(cliente));
        }

        var relatorio = await query
            .GroupBy(f => f.Cliente)
            .Select(g => new RelatorioCliente
            {
                Cliente = g.Key,
                TotalFaturas = g.Count(),
                TotalValor = g.Sum(f => f.FaturaItem.Select(x=>x.Valor).Sum())
            })
            .ToListAsync();

        return relatorio;
    }

    public async Task<IEnumerable<RelatorioAnoMes>> GerarRelatorioPorAnoMes(DateTime? dateInitial, DateTime? dateFinish)
    {
        var query = _context.Fatura.AsQueryable();

        if (dateInitial.HasValue)
        {
            query = query.Where(f => f.Data >= dateInitial.Value);
        }

        if (dateFinish.HasValue)
        {
            query = query.Where(f => f.Data <= dateFinish.Value);
        }

        var relatorio = await query
            .GroupBy(f => new { f.Data.Year, f.Data.Month })
            .Select(g => new RelatorioAnoMes
            {
                Ano = g.Key.Year,
                Mes = g.Key.Month,
                TotalFaturas = g.Count(),
                TotalValor = g.Sum(f => f.FaturaItem.Select(x => x.Valor).Sum())
            })
            .ToListAsync();

        return relatorio;
    }

    public async Task<IEnumerable<Fatura>> GerarTopFaturas(int quantidadeDeFaturas)
    {
        var topFaturas = await _context.Fatura
            .OrderByDescending(f => f.FaturaItem.Select(x=> x.Valor).Sum())
            .Take(quantidadeDeItems)
            .ToListAsync();

        return topFaturas;
    }

    public async Task<IEnumerable<FaturaItem>> GerarTopItens(int quantidadeDeItems)
    {
        var topItens = await _context.FaturaItem
            .OrderByDescending(i => i.Valor)
            .Take(quantidadeDeItems)
            .ToListAsync();

        return topItens;
    }




}
