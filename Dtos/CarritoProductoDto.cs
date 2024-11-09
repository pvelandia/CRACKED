using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Dtos
{
    public class CarritoProductoDto
    {
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public int IdPedido { get; set; }
        public int IdEstado{ get; set; }
        public int IdCliente { get; set; }
        public int? Cantidad { get; set; }
        public float Precio { get; set; }
        
    }
}