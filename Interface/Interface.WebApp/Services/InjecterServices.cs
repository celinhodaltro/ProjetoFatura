using Microsoft.EntityFrameworkCore;
using Server.BusinessLayer;
using Server.DataAcessObject;
using Server.DataAcessObject.Providers;
using Server.Entities;

namespace Interface.WebApp.Services;

public static class InjecterServices
{
    public static void InjectServices(this IServiceCollection services)
    {
        #region Providers
        services.AddScoped<FaturaProvider>();
        services.AddScoped<FaturaItemProvider>();
        #endregion

        #region Context
        services.AddDbContext<AppDbContext>();
        #endregion

        #region BusinessLayer
        services.AddScoped<FaturaBusinessLayer>();
        #endregion
    }

}
