using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SISLOG.Models;
using SISLOG.Data;

namespace SISLOG.Controllers
{
    public class AccesoController : Controller
    {
        private readonly SislogContext _context;
        public AccesoController(SislogContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario modelo)
        {
            if (!string.IsNullOrWhiteSpace(modelo.Nombre) && !string.IsNullOrWhiteSpace(modelo.Clave))
            {
                Usuario usuario = null;
                usuario = _context.Usuarios.FirstOrDefault(usr => usr.Nombre == modelo.Nombre);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Index","Privacy");
            }
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login","Acceso");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
