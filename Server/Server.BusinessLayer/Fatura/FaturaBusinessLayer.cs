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
            return await _faturaProvider.GetFaturaWithItens(faturaId);
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

        public async Task AdicionarFaturaItem(FaturaItem item)
        {
            item.Validate();
            _faturaItemProvider.Insert(item);
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

        public async Task<IEnumerable<RelatorioCliente>> GerarRelatorioPorCliente(string cliente)
        {
            return await _faturaProvider.GerarRelatorioPorCliente(cliente);
        }
        public async Task<IEnumerable<RelatorioAnoMes>> GerarRelatorioPorAnoMes(DateTime? dateInitial, DateTime? dateFinish)
        {
            return await _faturaProvider.GerarRelatorioPorAnoMes(dateInitial, dateFinish);
        }

        public async Task<IEnumerable<Fatura>> GerarTop10Faturas()
        {
            return await _faturaProvider.GerarTop10Faturas();
        }

        public async Task<IEnumerable<FaturaItem>> GerarTop10Itens()
        {
            return await _faturaProvider.GerarTop10Itens();
        }






    }
}
