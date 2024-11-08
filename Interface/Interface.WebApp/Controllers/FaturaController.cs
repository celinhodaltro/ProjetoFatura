using Microsoft.AspNetCore.Mvc;
using Server.BusinessLayer;
using Server.DataAcessObject.Providers;
using Server.Entities;
using Server.Entities.Model;
using System;
using System.Threading.Tasks;

public class FaturaController : Controller
{
    private readonly FaturaBusinessLayer _faturaBL;
    private readonly RelatorioBusinessLayer _relatorioBL;

    public FaturaController(FaturaBusinessLayer faturaBL, RelatorioBusinessLayer relatorioBL)
    {
        _faturaBL = faturaBL;
        _relatorioBL = relatorioBL;
    }

    public async Task<IActionResult> Index(FiltroFatura filtro)
    {
        filtro.Pagina = filtro.Pagina > 0 ? filtro.Pagina : 1;
        filtro.TamanhoPagina = filtro.TamanhoPagina > 0 ? filtro.TamanhoPagina : 10;

        try
        {
            var faturas = await _faturaBL.BuscarFaturasComFiltros(filtro);
            var totalFaturas = await _faturaBL.ContarFaturasComFiltros(filtro);
            var totalPaginas = (int)Math.Ceiling((double)totalFaturas / filtro.TamanhoPagina);

            var viewModel = new FaturasViewModel
            {
                Faturas = faturas,
                Filtro = filtro,
                TotalPaginas = totalPaginas
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    public async Task<IActionResult> Detalhes(int id)
    {
        var fatura = await _faturaBL.ObterFatura(id);
        return View(fatura);
    }

    public async Task<IActionResult> Editar(int id)
    {
        var fatura = await _faturaBL.ObterFatura(id);
        return View(fatura);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Fatura fatura)
    {
        await _faturaBL.AtualizarFatura(fatura);
        return RedirectToAction("Index", "Fatura");
    }

    public IActionResult Criar()
    {
        return View();
    }



    [HttpPost]
    public async Task<IActionResult> Criar(Fatura fatura)
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
    public async Task<IActionResult> Excluir(int id)
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
    public async Task<IActionResult> BuscarFaturas(FiltroFatura filtro)
    {
        try
        {
            var faturas = await _faturaBL.BuscarFaturasComFiltros(filtro);
            return Ok(faturas);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    public async Task<IActionResult> RelatorioPorCliente(string cliente)
    {
        try
        {
            var relatorio = await _faturaBL.GerarRelatorioPorCliente(cliente);
            var arquivo = _relatorioBL.GerarExcelRelatorioCliente(relatorio);
            return File(arquivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Relatorio_Cliente.xlsx");
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    public async Task<IActionResult> RelatorioPorAnoMes(DateTime? dataInicial, DateTime? dataFinal)
    {
        try
        {
            var relatorio = await _faturaBL.GerarRelatorioPorAnoMes(dataInicial, dataFinal);
            var arquivo = _relatorioBL.GerarExcelRelatorioAnoMes(relatorio);
            return File(arquivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Relatorio_Ano_Mes.xlsx");
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    public async Task<IActionResult> Top10Faturas()
    {
        try
        {
            var topFaturas = await _faturaBL.GerarTopFaturas(10);
            var arquivo = _relatorioBL.GerarExcelTop10Faturas(topFaturas);
            return File(arquivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Top10_Faturas.xlsx");
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    public async Task<IActionResult> Top10Itens()
    {
        try
        {
            var topItens = await _faturaBL.GerarTopItens(10);
            var arquivo = _relatorioBL.GerarExcelTop10Itens(topItens);
            return File(arquivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Top10_Itens.xlsx");
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}
