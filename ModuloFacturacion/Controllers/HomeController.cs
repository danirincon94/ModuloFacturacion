using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public IActionResult DetalleFactura(long id)
        {
            using (var context = new FacturacionContext())
            {
                var data = context.DetalleFactura.Include(x => x.IdFacturaNavigation).Include(x => x.IdFacturaNavigation.IdClienteNavigation).Include(x => x.IdProductoNavigation).Where(x => x.IdFactura == id).ToList();
                return View(data);
            }
        }

        public IActionResult CrearFactura()
        {
            using (var context = new FacturacionContext())
            {
                Factura fact = new Factura();
                fact.IdFactura = context.Factura.Max(x => x.IdFactura) + 1;
                fact.DetalleFactura = new Collection<DetalleFactura>();
                return View(fact);
            }

        }

        public JsonResult GuardarFactura([FromBody] Factura factura)
        {
            using (FacturacionContext context = new FacturacionContext())
            {
                if (factura == null)
                {
                    factura = new Factura();
                }

                Factura factura1 = new Factura();
                factura1.IdFactura = 2;
                factura1.IdCliente = 2;
                factura1.FechaCreacion = DateTime.Now;
                factura1.ValorTotal = 100;

                DetalleFactura det1 = new DetalleFactura();
                det1.IdFactura = 2;
                det1.IdProducto = 1;
                det1.IdDetalleFactura = 2;
                det1.Cantidad = 2;
                det1.ValorTotal = 200;
                factura1.DetalleFactura.Add(det1);

                //det1 = new DetalleFactura();
                //det1.IdFactura = 2;
                //det1.IdProducto = 2;
                //det1.IdDetalleFactura = 3;
                //det1.Cantidad = 2;
                //det1.ValorTotal = 300;
                //detallesfactura.Add(det1);
                //foreach (DetalleFactura detalle in detallefactura)
                //{
                //    context.DetalleFactura.Add(detalle);
                //}
                //int insertedRecords = context.SaveChanges();
                return Json(factura1);
            }
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
        [HttpPost]
        public JsonResult BuscarCliente(string nombre)
        {
            using (var context = new FacturacionContext())
            {
                var data = context.Cliente.Where(x => x.NombreCliente.Contains(nombre)).ToList();
                return Json(data);
            }
        }
    }
}
