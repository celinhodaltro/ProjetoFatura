using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class RelatorioAnoMes
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int TotalFaturas { get; set; }
        public double TotalValor { get; set; }
    }
}
