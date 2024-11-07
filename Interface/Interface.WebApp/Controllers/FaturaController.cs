using Microsoft.AspNetCore.Mvc;
using Server.BusinessLayer;
using Server.Entities;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class FaturaController : ControllerBase
{
    private readonly FaturaBusinessLayer _faturaBL;

    public FaturaController(FaturaBusinessLayer faturaBL)
    {
        _faturaBL = faturaBL;
    }

    [HttpPost]
    public async Task<IActionResult> AddFatura(Fatura fatura)
    {
        try
        {
            await _faturaBL.AdicionarFatura(fatura);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


}
