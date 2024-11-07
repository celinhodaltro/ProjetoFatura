using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities.Entities.Default
{
    public class DefaultDb
    {

        public int Id { get; set; }

        public virtual bool Validate()
        {
            return true;
        }
    }
}
