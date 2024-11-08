﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Fatura> ObterFatura(int faturaId)
        {
            return await _faturaProvider.ObterFatura(faturaId);
        }
        public async Task<int> ObterFaturasPorFaturaItemId(int faturaItemId)
        {
            return await _faturaProvider.ObterFaturaIdPorFaturaItemId(faturaItemId);
        }

        public async Task AtualizarFatura(Fatura faturaParaAtualizar)
        {
            faturaParaAtualizar.Validate();
            var fatura = await _faturaProvider.ObterFatura(faturaParaAtualizar.Id);
            fatura.Cliente = faturaParaAtualizar.Cliente;
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

        public async Task<List<Fatura>> BuscarFaturasComFiltros(FiltroFatura Filter)
        {
            var resultado = await _faturaProvider.BuscarFaturasComFiltros(Filter);
            if (resultado is { Count: 0 })
                return new();
            else
                return resultado;
        }

        public async Task<int> ContarFaturasComFiltros(FiltroFatura Filter)
        {
            return await _faturaProvider.ContarFaturasComFiltros(Filter);
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

        public async Task<IEnumerable<Fatura>> GerarTopFaturas(int quantidadeDeFaturas)
        {
            return await _faturaProvider.GerarTopFaturas(quantidadeDeFaturas);
        }

        public async Task<IEnumerable<FaturaItem>> GerarTopItens(int quantidadeDeItems)
        {
            return await _faturaProvider.GerarTopItens(quantidadeDeItems);
        }






    }
}
