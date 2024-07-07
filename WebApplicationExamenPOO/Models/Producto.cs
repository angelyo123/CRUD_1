using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationExamenPOO.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
    }
}