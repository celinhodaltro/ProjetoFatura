using Server.Entities.Entities.Default;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Server.Entities;

public class FaturaItem :DefaultDb
{

    public int FaturaId { get; set; }

    [Range(10, int.MaxValue, ErrorMessage = "A ordem deve ser múltiplo de 10.")]
    public int Ordem { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public double Valor { get; set; }

    [Required]
    public string Descricao { get; set; }

    [ForeignKey("FaturaId")]
    public virtual Fatura Fatura { get; set; }

    public FaturaItem()
    {
    }

    public override bool Validate()
    {
        StringBuilder Result = new("");

        if (this.Ordem % 10 != 0)
            Result.Append("A ordem deve ser multiplo de 10;");

        if (this.Descricao is null)
            Result.Append("Descrição obrigatoria!;");

        if (this.Descricao is { Length: > 20 })
            Result.Append("Descrição deve ser inferior á 20 caracteres;");

        if (this.Valor < 0)
            Result.Append("Valor deve ser positivo;");

        var Errors = Result.ToString();

        if (String.IsNullOrEmpty(Errors))
            return true;
        else
            throw new ArgumentException(Result.ToString());
    }


}
