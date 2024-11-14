using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Dtos
{
    public class ReportDto
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }

        public DateTime FechaEntrega { get; set; }
        public DateTime FechaVenta { get; set; }
        public String Direccion { get; set; }
        public String Barrio { get; set; }
        public String Telefono { get; set; }
        public float? ValorTotal { get; set; }
        public int IdUser { get; set; }
        public int IdRol { get; set; }
        public int IdEstado { get; set; }

        public string Name { get; set; }


        public string Apellido { get; set; }

        public string Numero { get; set; }

        public string Correo { get; set; }



    }
}