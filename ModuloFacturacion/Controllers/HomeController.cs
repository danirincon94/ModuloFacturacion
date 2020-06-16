using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModuloFacturacion.Models;

namespace ModuloFacturacion.Controllers
{
    public class HomeController : Controller
    {
        FacturacionContext db = new FacturacionContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var context = new FacturacionContext())
            {
                var data = context.Factura.Include(x => x.IdClienteNavigation).ToList();
                return View(data);
            }
        }

        public IActionResult DetalleFactura()
        {
            var data = db.DetalleFactura;
            return View(data);
        }

        public IActionResult CrearFactura()
        {
            var data = db.DetalleFactura;
            return View(data);
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
