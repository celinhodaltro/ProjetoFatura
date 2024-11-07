using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities;

public class FaturaFilter
{
    public string Cliente { get; set; } = String.Empty;
    public DateTime? DateInitial { get; set; }
    public DateTime? DateFinish{ get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
