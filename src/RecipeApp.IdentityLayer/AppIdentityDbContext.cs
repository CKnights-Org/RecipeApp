using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.IdentityLayer
{
    //create migrations with 
    //CLI: dotnet ef migrations add SomeName -s ../RecipeAppMVC/RecipeAppMVC.csproj -c AppIdentityDbContext
    //VS: Add-Migration SomeName -Context AppIdentityDbContext
    public class AppIdentityDbContext : IdentityDbContext
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
            
        }
    }
}