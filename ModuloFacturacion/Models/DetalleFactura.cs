using System;
using System.Collections.Generic;

namespace ModuloFacturacion.Models
{
    public partial class DetalleFactura
    {
        public long IdFactura { get; set; }
        public int IdProducto { get; set; }
        public long IdDetalleFactura { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorTotal { get; set; }

        public virtual Factura IdFacturaNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
