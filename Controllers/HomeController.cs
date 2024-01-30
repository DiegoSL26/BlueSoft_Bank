using Bluesoft_Bank.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bluesoft_Bank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly BankContext _context;

        public HomeController(ILogger<HomeController> logger, BankContext context)
        {
            _logger = logger;
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
        public JsonResult GetCliente(int? id)
        {
            return Json(_context.Clientes.Find());
        }

        [HttpGet]
        public JsonResult GetCuenta(int? id)
        {
            return Json(_context.Cuenta.Find(id));
        }
    }
}
