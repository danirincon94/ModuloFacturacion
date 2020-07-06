using ModuloFacturacion.Controllers;
using NUnit.Framework;

namespace UnitTestProject
{
    public class FacturacionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PruebaFacturas()
        {
            HomeController home = new HomeController();
            //home.DetalleFactura(2);
            home.AcumuladoFacturas();
        }
    }
}