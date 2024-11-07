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

        public async Task AdicionarFatura(Fatura fatura)
        {
            if (!fatura.Validate())
                throw new Exception("A fatura não é válida.");

            await _faturaProvider.Insert(fatura);
        }

        public async Task AdicionarFaturaItem(int faturaId, FaturaItem item)
        {
            var fatura = await _faturaProvider.GetFaturaWithItens(faturaId);
            item.Validate();
            

            fatura.FaturaItem.Add(item);
            await _faturaProvider.Update(fatura); 
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
