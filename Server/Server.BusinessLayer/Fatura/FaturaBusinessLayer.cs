using Microsoft.EntityFrameworkCore;
using Server.DataAcessObject.Providers;
using Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    public class FaturaBusinessLayer
    {
        private readonly FaturaProvider _faturaProvider;
        private readonly FaturaItemProvider _faturaItemProvider;

        public FaturaBusinessLayer(FaturaProvider faturaProvider, FaturaItemProvider faturaItemProvider)
        {
            _faturaProvider = faturaProvider;
            _faturaItemProvider = faturaItemProvider;
        }

        public async Task<Fatura> GetFaturaByIdAsync(int faturaId)
        {
            return await _faturaProvider.Get(faturaId);
        }

        public async Task UpdateFatura(Fatura fatura)
        {
            await _faturaProvider.Update(fatura);
        }

        public async Task AdicionarFatura(Fatura fatura)
        {
            fatura.Data = DateTime.Now;
            fatura.Validate();


            await _faturaProvider.Insert(fatura);
        }

        public async Task AdicionarFaturaItem(int faturaId, FaturaItem item)
        {
            var fatura = await _faturaProvider.GetFaturaWithItens(faturaId);
            item.Validate();
            

            fatura.FaturaItem.Add(item);
            await _faturaProvider.Update(fatura); 
        }

        public async Task<List<Fatura>> BuscarFaturasComFiltros(FaturaFilter Filter)
        {
            var resultado = await _faturaProvider.BuscarFaturasComFiltros(Filter);
            if (resultado is { Count: 0 })
                return new();
            else
                return resultado;
        }

        public async Task<int> CountFaturasComFiltros(FaturaFilter Filter)
        {
            return await _faturaProvider.CountFaturasComFiltros(Filter);
        }

        public async Task ExcluirFatura(int faturaId)
        {
            var fatura = await _faturaProvider.Get(faturaId);
            if (fatura != null)
                await _faturaProvider.Delete(fatura);
        }

        public async Task ExcluirFaturaItem(int faturaItemId)
        {
            var faturaItem = await _faturaItemProvider.Get(faturaItemId);
            if (faturaItem != null)
                await _faturaItemProvider.Delete(faturaItem);
        }


    }
}
