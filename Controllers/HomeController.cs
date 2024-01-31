using Bluesoft_Bank.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Bluesoft_Bank.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly BankContext _context;

        public HomeController(BankContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ClientPortal()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MonthlyStatement(int? id, DateOnly date)
        {
            var transacciones = _context.Transaccions.Include(t => t.Cliente).ToList();
            var transaccionesFiltradas = transacciones.Where(t => t.CuentaId == id && t.Fecha.Month == date.Month && t.Fecha.Year == date.Year).ToList();
            var transaccionesOrdenadas = transaccionesFiltradas.OrderBy(t => t.Fecha).ToList();
            return View(transaccionesOrdenadas);
        }

        [HttpGet]
        public JsonResult GetCliente(int? id)
        {
            return Json(_context.Clientes.Find(id));
        }

        [HttpGet]
        public JsonResult GetCuentasFromClient()
        {
            var id = ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id");
            if(id != null)
            {
                return Json(_context.Cuenta.Where(c => c.ClienteId == Convert.ToInt32(id.Value)).ToList());
            } else
            {
                return Json(null);
            }
        }

        [HttpGet]
        public JsonResult GetCuenta(int? id)
        {
            return Json(_context.Cuenta.Find(id));
        }

        [HttpGet]
        public JsonResult GetMovimientos(int? id)
        {
            return Json(_context.Transaccions
            .Where(t => t.CuentaId == id)
            .OrderByDescending(t => t.Fecha)
            .Take(5)
            .ToList());
        }
    }
}