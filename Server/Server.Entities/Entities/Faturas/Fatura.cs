using Server.Entities.Entities.Default;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Server.Entities;

public class Fatura :DefaultDb
{
    public Fatura()
    {
        FaturaItem = new HashSet<FaturaItem>();
    }

    [Required(ErrorMessage = "O campo Cliente é obrigatório.")]
    public string Cliente { get; set; }

    public DateTime Data { get; set; }

    public virtual ICollection<FaturaItem> FaturaItem { get; set; }

    public override bool Validate()
    {
        StringBuilder Result = new("");

        if (this.Cliente is null || String.IsNullOrEmpty(this.Cliente))
            Result.Append("O Cliente é obrigatorio;");

        var Errors = Result.ToString();

        if (String.IsNullOrEmpty(Errors))
            return true;
        else
            throw new InvalidDataException(Result.ToString());
    }

}