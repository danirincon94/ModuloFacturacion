using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModuloFacturacion.Models;

namespace ModuloFacturacion.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

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

        public IActionResult Pdf(long id)
        {
            FacturacionContext context = new FacturacionContext();

            var data = context.DetalleFactura.Include(x => x.IdFacturaNavigation).Include(x => x.IdFacturaNavigation.IdClienteNavigation).Include(x => x.IdProductoNavigation).Where(x => x.IdFactura == id).ToList();

            MemoryStream ms = new MemoryStream();

            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.LETTER);
            //doc.SetMargins(75, 35, 70, 35);

            Table table = new Table(1).UseAllAvailableWidth();

            Cell cell = new Cell().Add(new Paragraph("Factura de Venta").SetFontSize(16))
                .SetBorder(Border.NO_BORDER);
            table.AddCell(cell);

            //Datos Cliente
            cell = new Cell().Add(new Paragraph("Datos del Cliente").SetFontSize(13))
                .SetBorder(Border.NO_BORDER) 
                .SetPaddingTop(60);
            table.AddCell(cell);

            cell = new Cell().Add(new Paragraph("Cliente      " + data[0].IdFacturaNavigation.IdClienteNavigation.NombreCliente).SetFontSize(11))
                .SetBorder(Border.NO_BORDER);
            table.AddCell(cell);

            cell = new Cell().Add(new Paragraph("Telefono   " + data[0].IdFacturaNavigation.IdClienteNavigation.Telefono).SetFontSize(11))
                .SetBorder(Border.NO_BORDER);
            table.AddCell(cell);

            cell = new Cell().Add(new Paragraph("Fec. Nac. " + data[0].IdFacturaNavigation.IdClienteNavigation.FechaNacimiento).SetFontSize(11))
                .SetBorder(Border.NO_BORDER);
            table.AddCell(cell);

            //Datos Factura
            cell = new Cell().Add(new Paragraph("Datos de la Factura").SetFontSize(13))
               .SetBorder(Border.NO_BORDER)
               .SetPaddingTop(20);
            table.AddCell(cell);

            cell = new Cell().Add(new Paragraph("Número   " + data[0].IdFactura).SetFontSize(11))
                .SetBorder(Border.NO_BORDER);
            table.AddCell(cell);

            cell = new Cell().Add(new Paragraph("Fecha      " + data[0].IdFacturaNavigation.FechaCreacion).SetFontSize(11))
                .SetBorder(Border.NO_BORDER)
                .SetPaddingBottom(20);
            table.AddCell(cell);

            doc.Add(table);

            Table _table = new Table(4).UseAllAvailableWidth();
            Cell _cell = new Cell().Add(new Paragraph("Producto")).SetBold().SetFontSize(9).SetCharacterSpacing(1);
            _table.AddCell(_cell);
            _cell = new Cell().Add(new Paragraph("Precio")).SetBold().SetFontSize(9).SetCharacterSpacing(1);
            _table.AddCell(_cell);
            _cell = new Cell().Add(new Paragraph("Cantidad")).SetBold().SetFontSize(9).SetCharacterSpacing(1);
            _table.AddCell(_cell);
            _cell = new Cell().Add(new Paragraph("Valor")).SetBold().SetFontSize(9).SetCharacterSpacing(1);
            _table.AddCell(_cell);

            foreach (var item in data)
            {
                _cell = new Cell().Add(new Paragraph(item.IdProductoNavigation.Nombre)).SetFontSize(9);
                _table.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(string.Format("{0:C}", item.IdProductoNavigation.ValorUnitario))).SetFontSize(9);
                _table.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.Cantidad.ToString())).SetFontSize(9);
                _table.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(string.Format("{0:C}", item.IdFacturaNavigation.ValorTotal))).SetFontSize(9);
                _table.AddCell(_cell);
            }

            doc.Add(_table);
            doc.Close();

            byte[] bytesStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
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

        public void AcumuladoFacturas()
        {
            int suma = 200 + 300;
            Console.WriteLine(suma);
        }
    }
}
