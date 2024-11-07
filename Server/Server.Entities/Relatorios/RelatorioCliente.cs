using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities;
public class RelatorioCliente
{
    public string Cliente { get; set; }
    public int TotalFaturas { get; set; }
    public double TotalValor { get; set; }
}
