using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.DataAcessObject.Providers;

public class FaturaItemProvider : BaseProvider<FaturaItem>
{
    public FaturaItemProvider(DbContext context) : base(context) { }


}

