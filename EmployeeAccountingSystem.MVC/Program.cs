using EmployeeAccountingSystem.BLL.Infrastructure.DependencyInjection;
using EmployeeAccountingSystem.Shared.Configurations;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<Config>(configuration.GetSection("ConnectionStrings"));

builder.Services.AddStorage();

builder.Services.AddServices();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program));
    cfg.AddBllMaps();
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}
