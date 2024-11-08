using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities;

public class FaturasViewModel
{
    public IEnumerable<Fatura> Faturas { get; set; }
    public FiltroFatura Filtro { get; set; }
    public int TotalPaginas { get; set; }
}
