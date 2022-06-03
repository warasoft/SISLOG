using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SISLOG.Models;

namespace SISLOG.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ILogger<AccesoController> _logger;

        public AccesoController(ILogger<AccesoController> logger)
        {
            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario modelo)
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
