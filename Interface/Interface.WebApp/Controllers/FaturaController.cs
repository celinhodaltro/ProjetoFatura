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

    public async Task<IActionResult> Index()
    {
        var faturas = await _faturaBL.BuscarFaturasComFiltros(new FaturaFilter { Page = 0, PageSize = 10 });
        return View(faturas);
    }
    public IActionResult Details()
    {
        return View();
    }
    public IActionResult Edit()
    {
        return View();
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
            return Ok();
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
