using Bluesoft_Bank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bluesoft_Bank.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        private readonly BankContext _context;

        public AdminController(BankContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MonthReport()
        {
            return View();
        }

        public IActionResult WithdrawalReport()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetClientesConMasTransacciones(DateOnly date)
        {
            var transacciones = _context.Transaccions.Where(t => t.Fecha.Month == date.Month && t.Fecha.Year == date.Year).ToList();
            var clientes = _context.Clientes.ToList();
            
            var clientesConTransacciones = new List<Cliente>();
            foreach (var cliente in clientes)
            {
                if (cliente.Transacciones.Count > 0)
                {
                    clientesConTransacciones.Add(cliente);
                }
            }

            var clientesConMasTransacciones = clientesConTransacciones.OrderByDescending(c => c.Transacciones.Count).ToList();
            return Json(clientesConMasTransacciones);
        }

        [HttpGet]
        public JsonResult GetWithdrawalClientInfo()
        {
            var transacciones = _context.Transaccions.Include(t => t.Cuenta).Include(t => t.Cliente);
            var transaccionesFiltradasPorRetiro= transacciones.Where(t => t.TipoTransaccion == "Retiro").ToList();
            var transaccionesFiltradasPorMonto = transaccionesFiltradasPorRetiro.Where(t => t.Monto > 1000000).ToList();
            var transaccionesFiltradasPorCiudad = transaccionesFiltradasPorMonto.Where(t => t.Cuenta?.Ciudad != t.CiudadOrigen).ToList();
            return Json(transaccionesFiltradasPorCiudad);
        }

    }
}
