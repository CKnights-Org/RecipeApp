using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PizzaShopDAL.Data;
using RecipeAppMVC.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// registration of DBContext as SQLite
builder.Services.AddDbContext<RecipeAppDBContext>(o =>
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);

    // %appdata%\..\Local
    o
        .UseSqlite($"Data Source={Path.Join(path, "RecipeApp.db")}")
        .UseLazyLoadingProxies();
});

// custom mapster config
builder.Services.RegisterMapsterConfiguration();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
