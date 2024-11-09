using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Dtos
{
    public class PedidoDto
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public int IdDomiciliario { get; set; }
        public int IdCiudad { get; set; }
        public int IdMetodoPago { get; set; }
        public int IdEstado{ get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaVenta { get; set; }
        public String Direccion { get; set; }
        public float ValorTotal { get; set; }
        public float Iva { get; set; }
        public string Estado { get; set; }
       
  

}
}