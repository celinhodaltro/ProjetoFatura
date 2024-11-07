using Server.Entities.Entities.Default;
using System.ComponentModel.DataAnnotations;

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


}