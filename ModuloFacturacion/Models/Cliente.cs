using System;
using System.Collections.Generic;

namespace ModuloFacturacion.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Factura = new HashSet<Factura>();
        }

        public long IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public virtual ICollection<Factura> Factura { get; set; }
    }
}
