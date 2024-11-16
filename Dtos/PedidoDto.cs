using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CRACKED.Dtos
{
    public class PedidoDto
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public int IdDomiciliario { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String IdCiudad { get; set; }
        public int IdCiudads { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public int IdDepartamento { get; set; }
        public int IdEstado{ get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaVenta { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Direccion { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Barrio { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Telefono { get; set; }
        public float ValorTotal { get; set; }

        public string NombreCliente { get; set; }
        public string NombreDomiciliario { get; set; }
        public string NombreEstado { get; set; }
        public string NombreCiudad { get; set; }
        public string NombreDepartamento { get; set; }



    }
}