using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.DataAcessObject.Providers;

public class FaturaProvider : BaseProvider<Fatura>
{
    public FaturaProvider(DbContext context) : base(context) { }

    public async Task<Fatura> GetFaturaWithItens(int faturaId)
    {
        var result = await _dbSet
            .Include(f => f.FaturaItem)
            .FirstOrDefaultAsync(f => f.Id == faturaId);

        if (result is null)
            return new Fatura();

        return result;
    }

}
