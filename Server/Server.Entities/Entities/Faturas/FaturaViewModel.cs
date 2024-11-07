using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities;

public class FaturasViewModel
{
    public IEnumerable<Fatura> Faturas { get; set; }
    public FaturaFilter Filter { get; set; }
    public int TotalPages { get; set; }
}
