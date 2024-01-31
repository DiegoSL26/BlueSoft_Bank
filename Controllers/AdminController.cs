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
            var transaccionesFiltradasPorCiudad = transaccionesFiltradasPorRetiro.Where(t => t.Cuenta?.Ciudad != t.CiudadOrigen).ToList();
            var transaccionesPorClientes = transaccionesFiltradasPorCiudad.GroupBy(t => t.ClienteId).ToList();
            var clientes = new List<object>();
            foreach (var transaccion in transaccionesPorClientes)
            {
                var cliente = transaccion.First().Cliente;
                var totalRetiros = transaccion.Sum(t => (decimal)t.Monto);
                if(totalRetiros > 1000000)
                {
                    var clienteInfo = new
                    {
                        cliente?.Id,
                        cliente?.Nombre,
                        cliente?.Apellido,
                        TotalRetiros = totalRetiros
                    };
                    clientes.Add(clienteInfo);
                }
            }
           return Json(clientes);
     }

    }
}
