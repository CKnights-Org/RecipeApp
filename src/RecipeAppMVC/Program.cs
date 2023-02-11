using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.Data;
using RecipeAppMVC.Configs;
using RecipeApp.IdentityLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using RecipeAppMVC.Services;

var builder = WebApplication.CreateBuilder(args);

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
builder.Configuration["ConnectionStrings:DefaultConnection"] = $"Data Source={Path.Join(path, "RecipeApp.db")}";


// Add services to the container.
// registration of DBContext as SQLite
builder.Services.AddDbContext<RecipeAppDBContext>(o =>
{
    // %appdata%\..\Local
    o
        .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLazyLoadingProxies();
});

builder.Services.AddScoped<IRecipeService, RecipeService>();

builder.Services.AddAppIdentity(builder.Configuration);

// custom mapster config
builder.Services.RegisterMapsterConfiguration();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
