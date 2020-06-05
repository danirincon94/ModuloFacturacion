using System;
using System.Collections.Generic;

namespace ModuloFacturacion.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal ValorUnitario { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}
