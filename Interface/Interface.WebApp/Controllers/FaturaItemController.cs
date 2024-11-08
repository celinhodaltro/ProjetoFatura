using Microsoft.AspNetCore.Mvc;
using Server.BusinessLayer;
using Server.DataAcessObject.Providers;
using Server.Entities;
using Server.Entities.Model;
using System;
using System.Threading.Tasks;

public class FaturaItemController : Controller
{
    private readonly FaturaBusinessLayer _faturaBL;
    private readonly RelatorioBusinessLayer _relatorioBL;

    public FaturaItemController(FaturaBusinessLayer faturaBL, RelatorioBusinessLayer relatorioBL)
    {
        _faturaBL = faturaBL;
        _relatorioBL = relatorioBL;
    }


    public async Task<IActionResult> Detalhes(int id)
    {
        var fatura = await _faturaBL.ObterFaturas(id);
        return View(fatura);
    }

    public async Task<IActionResult> Editar(int id)
    {
        var fatura = await _faturaBL.ObterFaturas(id);
        return View(fatura);
    }




    public IActionResult Criar(int faturaId)
    {
        var faturaItem = new FaturaItem
        {
            FaturaId = faturaId
        };

        return View(faturaItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(FaturaItem faturaItem)
    {
        try
        {
            await _faturaBL.AdicionarFaturaItem(faturaItem);
            return RedirectToAction("Detalhes", "Fatura", new { id = faturaItem.FaturaId });
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", "Error", new ErrorModel(ex.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Excluir(int id)
    {
        try
        {
            var faturaId = await _faturaBL.ObterFaturasPorFaturaItemId(id);
            await _faturaBL.ExcluirFaturaItem(id);
            return RedirectToAction("Detalhes", "Fatura", new { Id = faturaId });
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", "Erro", new ErrorModel(ex.Message));
        }
    }




}
