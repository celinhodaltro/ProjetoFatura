using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.DataAcessObject.Providers;

public class FaturaItemProvider : BaseProvider<FaturaItem>
{
    public FaturaItemProvider(AppDbContext context) : base(context) { }


}

