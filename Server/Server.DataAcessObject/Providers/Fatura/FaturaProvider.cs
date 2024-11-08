using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.DataAcessObject.Providers;

public class FaturaProvider : BaseProvider<Fatura>
{
    public FaturaProvider(AppDbContext contexto) : base(contexto) { }

    public async Task<Fatura> ObterFatura(int faturaId)
    {
        var resultado = await _dbSet
            .Include(f => f.FaturaItem)
            .FirstOrDefaultAsync(f => f.Id == faturaId);

        if (resultado is null)
            return new Fatura();

        return resultado;
    }

    public async Task<int> ObterFaturaIdPorFaturaItemId(int faturaItemId)
    {
        var resultadoId = await this._context.FaturaItem.FirstOrDefaultAsync(x => x.Id == faturaItemId);

        return resultadoId?.FaturaId ?? 0;
    }


    public async Task<List<Fatura>> BuscarFaturasComFiltros(FiltroFatura filtro)
    {
        IQueryable<Fatura> consulta = _dbSet;

        if (!string.IsNullOrEmpty(filtro.Cliente))
            consulta = consulta.Where(f => f.Cliente.Contains(filtro.Cliente));

        if (filtro.DataInicial.HasValue)
            consulta = consulta.Where(f => f.Data >= filtro.DataInicial.Value);

        if (filtro.DataFinal.HasValue)
            consulta = consulta.Where(f => f.Data <= filtro.DataFinal.Value);

        if (filtro is { TamanhoPagina: > 0 })
            consulta = consulta.Skip((filtro.Pagina - 1) * filtro.TamanhoPagina).Take(filtro.TamanhoPagina);

        return await consulta.ToListAsync();
    }

    public async Task<int> ContarFaturasComFiltros(FiltroFatura filtro)
    {
        var consulta = _context.Fatura.AsQueryable();

        if (!string.IsNullOrEmpty(filtro.Cliente))
        {
            consulta = consulta.Where(f => f.Cliente.Contains(filtro.Cliente));
        }

        if (filtro.DataInicial.HasValue)
        {
            consulta = consulta.Where(f => f.Data >= filtro.DataInicial.Value);
        }

        if (filtro.DataFinal.HasValue)
        {
            consulta = consulta.Where(f => f.Data <= filtro.DataFinal.Value);
        }

        return await consulta.CountAsync();
    }

    public async Task<IEnumerable<RelatorioCliente>> GerarRelatorioPorCliente(string cliente)
    {
        var consulta = _context.Fatura.AsQueryable();

        if (!string.IsNullOrEmpty(cliente))
        {
            consulta = consulta.Where(f => f.Cliente.Contains(cliente));
        }

        var relatorio = await consulta
            .GroupBy(f => f.Cliente)
            .Select(g => new RelatorioCliente
            {
                Cliente = g.Key,
                TotalFaturas = g.Count(),
                TotalValor = g.Sum(f => f.FaturaItem.Select(x => x.Valor).Sum())
            })
            .ToListAsync();

        return relatorio;
    }

    public async Task<IEnumerable<RelatorioAnoMes>> GerarRelatorioPorAnoMes(DateTime? dataInicial, DateTime? dataFinal)
    {
        var consulta = _context.Fatura.AsQueryable();

        if (dataInicial.HasValue)
        {
            consulta = consulta.Where(f => f.Data >= dataInicial.Value);
        }

        if (dataFinal.HasValue)
        {
            consulta = consulta.Where(f => f.Data <= dataFinal.Value);
        }

        var relatorio = await consulta
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
        var topFaturas = await _context.Fatura.Include(x => x.FaturaItem)
            .OrderByDescending(f => f.FaturaItem.Select(x => x.Valor).Sum())
            .Take(quantidadeDeFaturas)
            .ToListAsync();

        return topFaturas;
    }

    public async Task<IEnumerable<FaturaItem>> GerarTopItens(int quantidadeDeItens)
    {
        var topItens = await _context.FaturaItem
            .OrderByDescending(i => i.Valor)
            .Take(quantidadeDeItens)
            .ToListAsync();

        return topItens;
    }
}
