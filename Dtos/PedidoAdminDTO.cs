﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRACKED.Dtos
{
    public class PedidoAdminDTO
    {
        public int IdPedido { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string ApellidoCliente { get; set; }
        public int IdEstado { get; set; }
        public string ClienteNombre { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Ciudad { get; set; }

        public string Telefono { get; set; }
        public string Correo { get; set; }
        public Double? ValorPedido { get; set; }

        public string Domiciliario { get; set; }
        
    }
        
    
}