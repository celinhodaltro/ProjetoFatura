


using Microsoft.EntityFrameworkCore;
using Server.DataAcessObject;
using Server.Entities;
using Server.DataAcessObject.Providers;

using (var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().Options))
{

    // Criando uma fatura de teste
    var newfatura = new Fatura
    {
        FaturaId = 1,
        Cliente = "Cliente Exemplo",
        Data = DateTime.Now,
        FaturaItem = new List<FaturaItem>
    {
        new FaturaItem
        {
            FaturaItemId = 1,
            FaturaId = 1,
            Ordem = 1,
            Valor = 100.50
        },
        new FaturaItem
        {
            FaturaItemId = 2,
            FaturaId = 1,
            Ordem = 2,
            Valor = 200.75
        },
        new FaturaItem
        {
            FaturaItemId = 3,
            FaturaId = 1,
            Ordem = 3,
            Valor = 50.00
        }
    }
    };



    var provider = new BaseProvider<Fatura>(context);
    provider.Insert(newfatura);

    context.Fatura.Add(newfatura);
    context.SaveChanges();

    var fatura = context.Fatura.FirstOrDefault();
    Console.WriteLine($"Fatura ID: {fatura.FaturaId}");
    Console.WriteLine($"Cliente: {fatura.Cliente}");
    Console.WriteLine($"Data: {fatura.Data}");
    Console.WriteLine("Itens da Fatura:");
    foreach (var item in fatura.FaturaItem)
    {
        Console.WriteLine($" - Item {item.Ordem}: {item.Valor:C}");
    }



}