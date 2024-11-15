using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CRACKED.Dtos
{
    public class Pedido_ProductoDto
    {
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public int IdPedido { get; set; }
        public int IdEstado{ get; set; }
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Selecciona la cantidad")]
        public int? Cantidad { get; set; }
        public int? Porcion { get; set; }
        public double Precio { get; set; }
        
    }
}