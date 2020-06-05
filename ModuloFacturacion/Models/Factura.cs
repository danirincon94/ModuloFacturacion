using System;
using System.Collections.Generic;

namespace ModuloFacturacion.Models
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }

        public long IdFactura { get; set; }
        public long IdCliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal ValorTotal { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}
