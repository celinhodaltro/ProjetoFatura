using Server.BusinessLayer;
using Server.DataAcessObject.Providers;
using Server.Entities;
using Interface.WebApp.Services;
using OfficeOpenXml;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.InjectServices();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Fatura}/{action=Index}/{id?}");

        app.Run();
    }


}
