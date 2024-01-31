using Bluesoft_Bank.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Bluesoft_Bank.Controllers
{
    public class AccessController : Controller
    {

        private readonly BankContext _context;

        public AccessController(BankContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ClientLogin(User user)
        {
            var client = _context.Clientes.Find(user.Id);
            if (client != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, client.Nombre),
                    new Claim("Id", client.Id.ToString())
                };

                foreach (var cuenta in client.Cuentas)
                {
                    claims.Add(new Claim("Cuentas", cuenta.Id.ToString()));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");

            } else
            {
                return RedirectToAction("Index", "Access");
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> AdminLogin(User user)
        {
            var admin = _context.Admins.Where(a => a.Email == user.Email && a.Password == user.Password).FirstOrDefault();
            if (admin != null)
            {

                var Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim("Id", admin.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Admin");
            } else
            {
                return RedirectToAction("Index", "Access");
            }
            
        }
        

        public async Task<IActionResult> Out()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Access");
        }
    }
}