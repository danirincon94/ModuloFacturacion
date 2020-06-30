﻿using System;
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
                factura.FechaCreacion = DateTime.Now;
                context.Factura.Add(factura);

                int insertedRecords = context.SaveChanges();
                return Json(insertedRecords);
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

        [HttpPost]
        public JsonResult BuscarProductoXNombre(string nombre)
        {
            using (var context = new FacturacionContext())
            {
                var data = context.Producto.Where(x => x.Nombre.Contains(nombre)).ToList();
                return Json(data);
            }
        }

        [HttpPost]
        public JsonResult BuscarProductoXId(Int32 id)
        {
            using (var context = new FacturacionContext())
            {
                var data = context.Producto.Where(x => x.IdProducto == id).ToList();
                return Json(data);
            }
        }
    }
}
