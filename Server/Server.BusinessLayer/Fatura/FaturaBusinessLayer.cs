using Server.DataAcessObject.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    public class FaturaBusinessLayer
    {
        public FaturaItemProvider? FaturaItemProvider { get; set; }
        public FaturaProvider? FaturaProvider { get; set; }

        

    }
}
