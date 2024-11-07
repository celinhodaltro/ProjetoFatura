using Microsoft.AspNetCore.Mvc;
using Server.BusinessLayer;
using Server.DataAcessObject.Providers;
using Server.Entities;
using Server.Entities.Model;
using System.Threading.Tasks;

public class FaturaController : Controller
{
    private readonly FaturaBusinessLayer _faturaBL;

    public FaturaController(FaturaBusinessLayer faturaBL)
    {
        _faturaBL = faturaBL;
    }

    public async Task<IActionResult> Index(FaturaFilter filter)
    {
        filter.Page = filter.Page > 0 ? filter.Page : 1;
        filter.PageSize = filter.PageSize > 0 ? filter.PageSize : 10;

        try
        {
            var faturas = await _faturaBL.BuscarFaturasComFiltros(filter);

            var totalFaturas = await _faturaBL.CountFaturasComFiltros(filter);
            var totalPages = (int)Math.Ceiling((double)totalFaturas / filter.PageSize);

            var viewModel = new FaturasViewModel
            {
                Faturas = faturas,
                Filter = filter,
                TotalPages = totalPages
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    public async Task<IActionResult> Details(int id)
    {
        var fatura = await _faturaBL.GetFaturaByIdAsync(id);
        return View(fatura);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var fatura = await _faturaBL.GetFaturaByIdAsync(id);
        return View(fatura);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Fatura fatura)
    {
        await _faturaBL.UpdateFatura(fatura);
        return RedirectToAction("Index", "Fatura");
    }


    public IActionResult Create()
    {
        return View();
    }
    public IActionResult CreateItem()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Fatura fatura)
    {
        try
        {
            await _faturaBL.AdicionarFatura(fatura);
            return RedirectToAction("Index", "Fatura");
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", "Error", new ErrorModel(ex.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _faturaBL.ExcluirFatura(id);
            return RedirectToAction("Index", "Fatura");
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", "Error", new ErrorModel(ex.Message));
        }
    }

    [HttpGet]
    public async Task<IActionResult> BuscarFaturas(FaturaFilter Filter)
    {
        try
        {
            var faturas = await _faturaBL.BuscarFaturasComFiltros(Filter);
            return Ok(faturas);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


}
