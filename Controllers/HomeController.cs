using ThuvienMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ThuvienMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index","Book");
        }
        public IActionResult Privacy()
        {
            return View();
        }


    }
}
