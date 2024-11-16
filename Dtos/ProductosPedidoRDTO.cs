using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRACKED.Dtos
{
    public class ProductosPedidoRDTO 
    {
            public int? IdPedidoProducto { get; set; }  // Nullable
            public int? IdPedido { get; set; }          // Nullable
            public int? IdCliente { get; set; }         // Nullable
            public int? IdProducto { get; set; }        // Nullable
            public int? Cantidad { get; set; }          // Nullable
            public int? IdEstado { get; set; }          // Nullable
            public Double? ValorProducto { get; set; } // Nullable
            public int? Porcion { get; set; }           // Nullable
            public DateTime? FechaEliminacion { get; set; }  // Nullable
         public string NombreProducto { get; set; }  // Nullable
        }
    }
