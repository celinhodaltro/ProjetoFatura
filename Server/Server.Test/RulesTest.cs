using Microsoft.EntityFrameworkCore;
using Server.DataAcessObject;
using Server.DataAcessObject.Providers;
using Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class FaturaProviderTests
{
    private readonly FaturaProvider _faturaProvider;
    private readonly AppDbContext _context;

    public FaturaProviderTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "FaturaTestDB")
            .Options;

        _context = new AppDbContext(options);
        _faturaProvider = new FaturaProvider(_context);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Fatura.AddRange(new List<Fatura>
        {
            new Fatura
            {
                Id = 1,
                Cliente = "Cliente A",
                Data = new DateTime(2023, 10, 18),
                FaturaItem = new List<FaturaItem>
                {
                    new FaturaItem { Id = 1, Valor = 100 , Descricao="Fatura 1" , FaturaId=1},
                    new FaturaItem { Id = 2, Valor = 200 , Descricao="Fatura 2" , FaturaId=1}
                }
            },
            new Fatura
            {
                Id = 2,
                Cliente = "Cliente B",
                Data = new DateTime(2023, 10, 19),
                FaturaItem = new List<FaturaItem>
                {
                    new FaturaItem { Id = 3, Valor = 300, Descricao="Fatura 3", FaturaId=2}
                }
            }
        });
        _context.SaveChanges();
    }

    [Fact]
    public async Task ObterFatura_DeveRetornarFatura_ComItens()
    {

        var fatura = await _faturaProvider.ObterFatura(1);

        Assert.NotNull(fatura);
        Assert.Equal("Cliente A", fatura.Cliente);
        Assert.Equal(2, fatura.FaturaItem.Count);
    }

    [Fact]
    public async Task ObterFaturaIdPorFaturaItemId_DeveRetornarIdCorreto()
    {
        var faturaId = await _faturaProvider.ObterFaturaIdPorFaturaItemId(3);

        Assert.Equal(2, faturaId);
    }

    [Fact]
    public async Task BuscarFaturasComFiltros_DeveRetornarFaturasFiltradas()
    {
        var filtro = new FiltroFatura
        {
            Cliente = "Cliente A",
            DataInicial = new DateTime(2023, 10, 17),
            DataFinal = new DateTime(2023, 10, 19),
            Pagina = 1,
            TamanhoPagina = 10
        };

        var resultado = await _faturaProvider.BuscarFaturasComFiltros(filtro);

        Assert.Single(resultado);
        Assert.Equal("Cliente A", resultado.First().Cliente);
    }

    [Fact]
    public void AdicionarItem_ValorPositivo()
    {
        var fatura = new Fatura { Cliente = "Cliente 1" };
        var item = new FaturaItem { Valor = 100, Ordem = 10, Descricao = "Item 1" };

        

        Assert.True(item.Validate());
    }

    [Fact]
    public void AdicionarItem_ValorNegativo_DeveLancarExcecao()
    {
        var item = new FaturaItem { Valor = -10, Ordem = 10, Descricao = "Item 1" };

        
        Assert.Throws<ArgumentException>(() => item.Validate());
    }

    [Fact]
    public void AdicionarItem_OrdemNaoMultiploDe10_DeveLancarExcecao()
    {
        var item = new FaturaItem { Valor = 100, Ordem = 15, Descricao = "Item 1" };

        Assert.Throws<ArgumentException>(() => item.Validate());
    }

    [Fact]
    public void AdicionarItem_DescricaoLonga_DeveLancarExcecao()
    {
        var item = new FaturaItem { Valor = 100, Ordem = 10, Descricao = "Item que excede o limite de caracteres" };

        Assert.Throws<ArgumentException>(() => item.Validate());
    }


    [Fact]
    public void AdicionarFatura_ClienteObrigatorio_DeveLancarExcecao()
    {
        var fatura = new Fatura { Cliente = "" };

        Assert.Throws<ArgumentException>(() => fatura.Validate());
    }
}
