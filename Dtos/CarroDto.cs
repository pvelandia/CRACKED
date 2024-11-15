using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Dtos
{
    public class CarroDto
    {
        public int IdProducto { get; set; }
        public int IdPedidoProducto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public float PrecioUnit { get; set; }
        public float PrecioTotal { get; set; }
        public float PrecioSubtotal { get; set; }
        public string Imagen { get; set; }
    }
}