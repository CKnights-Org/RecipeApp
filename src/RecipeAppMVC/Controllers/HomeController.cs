using Microsoft.AspNetCore.Mvc;
using PizzaShopDAL.Data;
using RecipeAppMVC.Models;
using System.Diagnostics;

namespace RecipeAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecipeAppDBContext _dBContext;

        public HomeController(ILogger<HomeController> logger, RecipeAppDBContext dBContext)
        {
            _logger = logger;
            _dBContext = dBContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}