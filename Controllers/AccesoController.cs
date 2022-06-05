using Microsoft.AspNetCore.Mvc;
using SISLOG.Models;
using SISLOG.Data;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

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
                usuario = _context.Usuarios.FirstOrDefault(usr => usr.Nombre == modelo.Nombre && usr.Clave== modelo.Clave);

                if (usuario != null)
                {
                    // Se crean las credenciales del usuario que serán incorporadas al contexto
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    // El lo que luego obtendré al acceder a User.Identity.Name
                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));

                    // Lo utilizaremos para acceder al Id del usuario que se encuentra en el sistema.
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                    // Se utilizará para la autorización por roles
                    identity.AddClaim(new Claim(ClaimTypes.Role, usuario.Rol));

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    // En este paso se hace el login del usuario al sistema
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();
                    
                    return RedirectToAction("Index", "Home");
                }
                return View();
                
            }
            else
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction("Login","Acceso");
        }
    }
}
