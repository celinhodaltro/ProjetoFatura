using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities;

public class FiltroFatura
{
    public string Cliente { get; set; } = String.Empty;
    public DateTime? DataInicial {get; set;}
    public DateTime? DataFinal {get; set;}
    public int Pagina { get; set; }
    public int TamanhoPagina { get; set; }
}
