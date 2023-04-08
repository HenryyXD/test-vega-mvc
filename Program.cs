using TestVegaMVC;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    var conStr = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(conStr, ServerVersion.AutoDetect(conStr));
});

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Supplier}/{action=Index}/{id?}");

app.Run();
